using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web;
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
                string query = "SELECT * FROM Users WHERE IDNumber = @idnumber AND hashPassword = @password";

                /* using (MD5 md5Hash = MD5.Create())
                {
                    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
                    inputPassword = BitConverter.ToString(data).Replace("-", string.Empty);
                }*/

                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@idnumber", id);
                    command.Parameters.AddWithValue("@password", password);

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

        public User searchStudentData(int userID, string pathDB)
        {
            User student = new User();

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
                            student.Dob = Convert.ToDateTime(reader["DoB"]);
                            student.Nationality = reader["Nationality"].ToString();
                            student.IDNumber = reader["IDNumber"].ToString();
                            student.Address = reader["Address"].ToString();
                            student.UserType = reader["UserType"].ToString();
                        }
                    }
                }

                string queryForStudents = "SELECT " +
                "d.degreeName AS Degree, " +
                "GROUP_CONCAT(DISTINCT s.semester) AS Semesters, " +
                "SUM(s.credits) AS TotalCredits " +
                "FROM " +
                "Users u " +
                "JOIN Student_Subjects ss ON u.UserID = ss.UserID " +
                "JOIN Subjects s ON ss.subjectID = s.subjectID " +
                "JOIN Degree d ON ss.degreeID = d.degreeID " +
                "WHERE " +
                "u.UserID = @userID " +
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

                            student.SubjectsProfessors += $"{professorName} - {subjectName}\n";
                        }
                    }
                }
            }
            return student;
        }

        public bool editUserStudent(User editedUser, int userID, string pathDB)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();
                string query = "UPDATE Users SET Name = @editedName, Surname = @editedSurname, DoB = @editedDob, " +
                               "Nationality = @editedNationality, IDNumber = @editedId, Address = @editedAddress WHERE UserID = @userID";

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

        public string searchProfessorData(int userID, string pathDB)
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
                            subjectsFromProfessor += reader["SubjectName"].ToString();
                        }
                    }
                }
            }

            return subjectsFromProfessor;
        }

        public int getSubjectId(string subjectName, string pathDB)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();

                string subjectIdQuery = "SELECT subjectID FROM Subjects WHERE name = @subjectName";

                using (SQLiteCommand subjectIdCommand = new SQLiteCommand(subjectIdQuery, conn))
                {
                    subjectIdCommand.Parameters.AddWithValue("@subjectName", subjectName);

                    int subjectID = Convert.ToInt32(subjectIdCommand.ExecuteScalar());
                    return subjectID;
                }
            }
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
                            studentsForSubject += reader["StudentName"].ToString() + " " + reader["StudentSurname"].ToString() + "\n";
                        }
                    }
                }
            }

            return studentsForSubject;
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
                        }
                    }
                }
            }

            return subject;
        }


    }
}