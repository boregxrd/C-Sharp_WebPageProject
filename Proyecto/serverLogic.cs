using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Proyecto
{
    public class ServerLogic
    {
        public bool login(string id, string password, string pathDB, string[] receivedData)
        {

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE IDNumber = @idnumber AND hashPassword = @hashedPassword";

                string hashedPassword = HashPassword(password);

                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@idnumber", id);
                    command.Parameters.AddWithValue("@hashedPassword", hashedPassword);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string userType = reader["userType"].ToString();
                            receivedData[0] = userType;
                            receivedData[1] = reader["userID"].ToString();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }

        public User searchPersonalData(int userID, string pathDB)
        {
            User student = new User();
            string subjectprofessoraux = "";

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE UserID = @userID";

                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@userID", userID);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            student.UserID = Convert.ToInt32(reader["UserID"]);
                            student.Name = reader["Name"].ToString();
                            student.Surname = reader["Surname"].ToString();
                            student.IDNumber = reader["IDNumber"].ToString();
                            student.UserType = reader["UserType"].ToString();

                            if (reader["DoB"] != DBNull.Value && reader["DoB"] != null)
                            {
                                student.Dob = Convert.ToDateTime(reader["DoB"]);
                            }
                            else
                            {
                                student.Dob = DateTime.MinValue;
                            }
                            if (reader["Nationality"] != DBNull.Value && reader["Nationality"] != null)
                            {
                                student.Nationality = reader["Nationality"].ToString();
                            }
                            else
                            {
                                student.Nationality = "";
                            }

                            if (reader["Address"] != DBNull.Value && reader["Address"] != null)
                            {
                                student.Address = reader["Address"].ToString();
                            }
                            else
                            {
                                student.Address = "";
                            }
                        }
                        else
                        {
                            // Initialize default values if no results are found
                            student.Dob = DateTime.MinValue;
                            student.Nationality = "";
                            student.Address = "";
                        }
                    }
                }
                string queryForStudents = "SELECT " +
                "COALESCE(d.degreeName, '') AS Degree, " +
                "(SELECT GROUP_CONCAT(DISTINCT s.semester) FROM Subjects s " +
                "JOIN Student_Subjects ss ON s.subjectID = ss.subjectID " +
                "WHERE ss.userID = u.userID ORDER BY s.semester) AS Semesters, " +
                "COALESCE(SUM(s.credits), 0) AS TotalCredits " +
                "FROM Users u " +
                "LEFT JOIN Student_Subjects ss ON u.UserID = ss.UserID " +
                "LEFT JOIN Subjects s ON ss.subjectID = s.subjectID " +
                "LEFT JOIN Degree d ON ss.degreeID = d.degreeID " +
                "WHERE u.UserID = @userID " +
                "GROUP BY d.degreeName";

                using (SQLiteCommand command = new SQLiteCommand(queryForStudents, conn))
                {
                    command.Parameters.AddWithValue("@userID", userID);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            student.Degree = reader["Degree"].ToString();
                            student.Credits = Convert.ToInt32(reader["TotalCredits"]);
                            student.Semester = reader["Semesters"].ToString();
                        }
                    }
                }


                string queryForSubjectsProfessor = "SELECT " +
                "p.Name AS ProfessorName, " +
                "s.name AS SubjectName " +
                "FROM " +
                "Teacher_Subjects ts " +
                "JOIN Users p ON ts.userID = p.UserID " +
                "JOIN Subjects s ON ts.subjectID = s.subjectID";

                using (SQLiteCommand command = new SQLiteCommand(queryForSubjectsProfessor, conn))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string professorName = reader["ProfessorName"].ToString();
                            string subjectName = reader["SubjectName"].ToString();

                            subjectprofessoraux += $"{professorName} - {subjectName}" + ",";
                        }
                    }
                }
            }
            student.SubjectsProfessors = subjectprofessoraux.Split(',');
            return student;
        }
        public string GetStudentsForSubject(int subjectID, string pathDB)
        {
            string studentsForSubject = string.Empty;

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();

                string query = "SELECT u.Name AS StudentName, u.Surname AS StudentSurname " +
                               "FROM Student_Subjects ss " +
                               "JOIN Users u ON ss.userID = u.UserID " +
                               "WHERE ss.subjectID = @subjectID";

                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@subjectID", subjectID);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            studentsForSubject += reader["StudentName"].ToString() + " " + reader["StudentSurname"].ToString() + ",";
                        }
                    }
                }
            }

            return studentsForSubject;
        }


        public bool editUser(User editedUser, int userID, bool adminEdit, string pathDB)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();
                string query = "UPDATE Users SET Name = @editedName, Surname = @editedSurname, DoB = @editedDob, " +
                               "Nationality = @editedNationality, IDNumber = @editedId, Address = @editedAddress WHERE UserID = @userID";

                string queryForAdmin = "UPDATE Users SET Name = @editedName, IDNumber = @editedId WHERE UserID = @userID";

                if (adminEdit)
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryForAdmin, conn))
                    {
                        command.Parameters.AddWithValue("@editedName", editedUser.Name);
                        command.Parameters.AddWithValue("@editedId", editedUser.IDNumber);
                        command.Parameters.AddWithValue("@userID", userID);
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
                else
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@editedName", editedUser.Name);
                        command.Parameters.AddWithValue("@editedSurname", editedUser.Surname);
                        command.Parameters.AddWithValue("@editedDob", editedUser.Dob);
                        command.Parameters.AddWithValue("@editedNationality", editedUser.Nationality);
                        command.Parameters.AddWithValue("@editedId", editedUser.IDNumber);
                        command.Parameters.AddWithValue("@userID", userID);
                        command.Parameters.AddWithValue("@editedAddress", editedUser.Address);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
        }

        public bool editSubject(Subject subject, int subjectId, string pathDB)
        {
            int degreeID = GetDegreeId(subject.Degree, pathDB);
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();
                string query = "UPDATE Subjects SET Credits = @editedCredits, Semester = @editedSemester, " +
                    "DegreeID = @degreeID WHERE SubjectId = @subjectId";

                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@editedCredits", subject.Credits);
                    command.Parameters.AddWithValue("@editedSemester", subject.Semester);
                    command.Parameters.AddWithValue("@degreeID", degreeID);
                    command.Parameters.AddWithValue("@subjectId", subjectId);
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        public string[] searchProfessorData(int userID, string pathDB)
        {
            string subjectsFromProfessor = string.Empty;

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();
                string query = "SELECT s.name AS SubjectName " +
                               "FROM Teacher_Subjects ts " +
                               "JOIN Subjects s ON ts.subjectID = s.subjectID " +
                               "WHERE ts.userID = @userID";

                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@userID", userID);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subjectsFromProfessor += reader["SubjectName"].ToString() + ",";
                        }
                    }
                }
            }

            return subjectsFromProfessor.Split(',');
        }

        public int getItemId(string itemName, string role, string pathDB)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                string itemIdQuery;
                conn.Open();
                if (role == "null")
                {
                    itemIdQuery = "SELECT subjectID FROM Subjects WHERE name = @itemName";
                }
                else
                {
                    itemIdQuery = "SELECT userID FROM Users WHERE name = @itemName AND userType = @role";
                }

                using (SQLiteCommand itemIdCommand = new SQLiteCommand(itemIdQuery, conn))
                {
                    itemIdCommand.Parameters.AddWithValue("@itemName", itemName);
                    if (role == "null")
                    {
                        using (SQLiteDataReader reader = itemIdCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int itemID = Convert.ToInt32(reader["subjectID"]);
                                return itemID;
                            }
                        }
                    } else {
                        itemIdCommand.Parameters.AddWithValue("@role", role);
                        using (SQLiteDataReader reader = itemIdCommand.ExecuteReader())
                        {
                            if (reader.Read()) 
                            {
                                int itemID = Convert.ToInt32(reader["userID"]); 
                                return itemID;
                            }
                        }
                    }
                }
            }

            return -1;
        }


        public Subject getSubjectData(int subjectID, string pathDB)
        {
            Subject subject = new Subject();

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();
                string query = "SELECT s.SubjectID, s.Name AS SubjectName, s.Credits, s.DegreeID, s.Semester, d.DegreeName " +
                               "FROM Subjects s " +
                               "JOIN Degree d ON s.DegreeID = d.DegreeID " +
                               "WHERE s.SubjectID = @subjectID";

                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@subjectID", subjectID);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subject.Credits = Convert.ToInt32(reader["Credits"]);
                            subject.Semester = Convert.ToInt32(reader["Semester"]);
                            subject.Degree = reader["DegreeName"].ToString();
                            subject.SubjectID = subjectID;
                        }
                    }
                }
            }

            return subject;
        }

        public string[] getAllFromTable(string tableName, string userType, string pathDB)
        {
            string result = "";

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();

                string query = "";

                switch (tableName)
                {
                    case "Subjects":
                        query = "SELECT * FROM Subjects";
                        break;
                    case "Users":
                        if (!string.IsNullOrEmpty(userType))
                        {
                            query = $"SELECT * FROM Users WHERE userType = '{userType}'";
                        }
                        else
                        {
                            query = "SELECT * FROM Users";
                        }
                        break;

                    default:
                        break;
                }

                if (!string.IsNullOrEmpty(query))
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result += reader["name"].ToString() + ",";
                            }
                        }
                    }
                }
            }

            return result.Split(',');
        }

        public bool deleteItem(int itemId, string role, string pathDB)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                string deleteQuery;
                conn.Open();

                switch (role)
                {
                    case "null":
                        deleteQuery = "DELETE FROM Subjects WHERE subjectID = @itemId";
                        break;

                    case "Student":
                    case "Professor":
                        deleteQuery = "DELETE FROM Users WHERE userID = @itemId AND userType = @role";
                        break;

                    default:
                        return false;
                }

                using (SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, conn))
                {
                    deleteCommand.Parameters.AddWithValue("@itemId", itemId);
                    if (role != "null")
                    {
                        deleteCommand.Parameters.AddWithValue("@role", role);
                    }
                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        private bool CreateDegree(string degreeName, string pathDB)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();

                string insertDegreeQuery = "INSERT INTO Degree (DegreeName) VALUES (@degreeName)";

                using (SQLiteCommand insertDegreeCommand = new SQLiteCommand(insertDegreeQuery, conn))
                {
                    insertDegreeCommand.Parameters.AddWithValue("@degreeName", degreeName);

                    int rowsAffected = insertDegreeCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        private int GetDegreeId(string degreeName, string pathDB)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();

                string query = "SELECT degreeID FROM Degree WHERE degreeName = @degreeName";

                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@degreeName", degreeName);

                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int degreeID))
                    {
                        return degreeID;
                    }

                    return -1;
                }
            }
        }



        public bool createSubject(Subject newSubject, string teacherName, string pathDB)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();

                int teacherID = getItemId(teacherName, "Professor", pathDB);
                if (teacherID == -1)
                {
                    return false;
                }

                int degreeID = GetDegreeId(newSubject.Degree, pathDB);
                if (degreeID == -1)
                {
                    if (!CreateDegree(newSubject.Degree, pathDB))
                    {
                        return false;
                    }

                    degreeID = GetDegreeId(newSubject.Degree, pathDB);
                }

                string insertSubjectQuery = "INSERT INTO Subjects(name, degreeID, semester, credits) VALUES(@subjectName, @degreeID, @semester, @credits); " +
                    "INSERT INTO Teacher_Subjects(userID, subjectID) VALUES(@teacherID, last_insert_rowid()); "
;

                using (SQLiteCommand insertSubjectCommand = new SQLiteCommand(insertSubjectQuery, conn))
                {
                    insertSubjectCommand.Parameters.AddWithValue("@subjectName", newSubject.Name);
                    insertSubjectCommand.Parameters.AddWithValue("@degreeID", degreeID);
                    insertSubjectCommand.Parameters.AddWithValue("@teacherID", teacherID);
                    insertSubjectCommand.Parameters.AddWithValue("@semester", newSubject.Semester);
                    insertSubjectCommand.Parameters.AddWithValue("@credits", newSubject.Credits);

                    int rowsAffected = insertSubjectCommand.ExecuteNonQuery();


                    return rowsAffected > 0;
                }
            }
        }

        public bool createUser(User newUser, string password, string pathDB)
        {
            string hashedPassword = HashPassword(password);

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();

                string insertUserQuery = "INSERT INTO Users (Name, Surname, IDNumber, UserType, hashPassword) " +
                                         "VALUES (@Name, @Surname, @IDNumber, @UserType, @HashPassword)";

                using (SQLiteCommand insertUserCommand = new SQLiteCommand(insertUserQuery, conn))
                {
                    insertUserCommand.Parameters.AddWithValue("@Name", newUser.Name);
                    insertUserCommand.Parameters.AddWithValue("@Surname", newUser.Surname);
                    insertUserCommand.Parameters.AddWithValue("@IDNumber", newUser.IDNumber);
                    insertUserCommand.Parameters.AddWithValue("@UserType", newUser.UserType);
                    insertUserCommand.Parameters.AddWithValue("@HashPassword", hashedPassword);

                    int rowsAffected = insertUserCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public string[] filterStudents(int subjectID, string pathDB)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();

                string query = "SELECT u.Name AS StudentName, u.Surname AS StudentSurname " +
                               "FROM Student_Subjects ss " +
                               "JOIN Users u ON ss.userID = u.UserID " +
                               "WHERE ss.subjectID = @subjectID";

                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@subjectID", subjectID);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        string students = "";
                        while (reader.Read())
                        {
                            students += reader["StudentName"].ToString() + ",";
                        }
                        return students.Split(',');
                    }
                }
            }
        }

        public string[] filterProfessors(int subjectID, string pathDB)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();

                string query = "SELECT u.Name AS ProfessorName, u.Surname AS ProfessorSurname " +
                               "FROM Teacher_Subjects ts " +
                               "JOIN Users u ON ts.userID = u.UserID " +
                               "WHERE ts.subjectID = @subjectID";

                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@subjectID", subjectID);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        string professors = "";
                        while (reader.Read())
                        {
                            professors += reader["ProfessorName"].ToString() + ',';
                        }
                        return professors.Split(',');
                    }
                }
            }
        }

        public bool AssignUserToSubject(int userID, int subjectID, string role, string pathDB)
        {
            if(userID == -1) { return false; }
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();

                string tableName;
                string userIDColumnName;
                string subjectIDColumnName;

                if (!GetTableColumnNames(role, out tableName, out userIDColumnName, out subjectIDColumnName))
                {
                    return false;
                }

                if (IsUserAssigned(conn, tableName, userIDColumnName, subjectIDColumnName, userID, subjectID))
                {
                    return true;
                }

                return InsertAssignment(conn, tableName, userIDColumnName, subjectIDColumnName, userID, subjectID);
            }
        }

        private bool GetTableColumnNames(string role, out string tableName, out string userIDColumnName, out string subjectIDColumnName)
        {
            tableName = null;
            userIDColumnName = null;
            subjectIDColumnName = null;

            if (role.Equals("Professor", StringComparison.OrdinalIgnoreCase))
            {
                tableName = "Teacher_Subjects";
                userIDColumnName = "userID";
                subjectIDColumnName = "subjectID";
            }
            else if (role.Equals("Student", StringComparison.OrdinalIgnoreCase))
            {
                tableName = "Student_Subjects";
                userIDColumnName = "userID";
                subjectIDColumnName = "subjectID";
            }
            else
            {
                return false;
            }

            return true;
        }

        private bool IsUserAssigned(SQLiteConnection conn, string tableName, string userIDColumnName, string subjectIDColumnName, int userID, int subjectID)
        {
            string checkAssignmentQuery = $"SELECT * FROM {tableName} WHERE {userIDColumnName} = @userID AND {subjectIDColumnName} = @subjectID";

            using (SQLiteCommand checkAssignmentCommand = new SQLiteCommand(checkAssignmentQuery, conn))
            {
                checkAssignmentCommand.Parameters.AddWithValue("@userID", userID);
                checkAssignmentCommand.Parameters.AddWithValue("@subjectID", subjectID);

                using (SQLiteDataReader reader = checkAssignmentCommand.ExecuteReader())
                {
                    return reader.Read();
                }
            }
        }

        private bool InsertAssignment(SQLiteConnection conn, string tableName, string userIDColumnName, string subjectIDColumnName, int userID, int subjectID)
        {
            string insertAssignmentQuery = $"INSERT INTO {tableName} ({userIDColumnName}, {subjectIDColumnName}) VALUES (@userID, @subjectID)";

            using (SQLiteCommand insertAssignmentCommand = new SQLiteCommand(insertAssignmentQuery, conn))
            {
                insertAssignmentCommand.Parameters.AddWithValue("@userID", userID);
                insertAssignmentCommand.Parameters.AddWithValue("@subjectID", subjectID);

                int rowsAffected = insertAssignmentCommand.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
    }
}