<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Proyecto.WebForm1" %>

    <!DOCTYPE html>
    <html lang="en">

    <head>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <title>Techville University</title>
        <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Montserrat&display=swap">
        <link rel="stylesheet" href="styles.css" />
    </head>

    <body>
        <form id="form1" runat="server">

            <header>
                <div class="logo">
                    <img src="img/techvilleLogo.png" alt="Techville University Logo" />
                </div>
                <nav>
                    <ul>
                        <li><a href="#">Home</a></li>
                        <li><a href="#">About Us</a></li>
                        <li><a href="#">Academics</a></li>
                        <li><a href="#">Admissions</a></li>
                        <li><a href="#">Research</a></li>
                        <li><a href="#">Campus Life</a></li>
                        <li><a href="#">Contact</a></li>
                    </ul>
                </nav>
                <div class="header-button-container">
                    <asp:Button runat="server" Text="Login" CssClass="button" OnClick="loginButton_Click"
                        ID="loginButton" />
                </div>
            </header>

            <div class="hero-section">
                <div class="background-image">
                    <img src="img/techvilleUniversity.png" alt="Background Image" />
                    <div class="overlay"></div>
                </div>
                <div class="content-overlay">
                    <p class="welcome-message">
                        Welcome to Techville University, where innovation meets education. Join us on a journey of
                        discovery and excellence in technology.
                    </p>
                </div>
            </div>

            <div class="content">
                <aside class="sidebar" id="sidebar-left">
                    <div class="quick-links">
                        <h2>Quick Links</h2>
                        <a href="#">Academic Calendar</a>
                        <a href="#">Campus Map</a>
                        <a href="#">Student Resources</a>
                    </div>

                    <div class="social-media-feeds">
                        <h2>Join the Conversation</h2>
                        <p>Follow us on social media for real-time updates, behind-the-scenes, and community stories.
                        </p>
                    </div>
                </aside>

                <div class="main-content">
                    <div class="featured-programs">
                        <h2>Explore Our Programs</h2>
                        <p>Dive into the future with our programs in Computer Science, Robotics, Artificial
                            Intelligence, and more.</p>
                    </div>

                    <div class="admissions-info">
                        <h2>Start Your Journey</h2>
                        <p>Learn about our admissions process, application requirements, and key dates. Your future
                            at
                            Techville begins here.</p>
                    </div>

                    <div class="latest-news">
                        <h2>Stay Informed</h2>
                        <p>Read about the latest achievements, breakthroughs, and announcements from Techville
                            University.</p>
                    </div>

                    <div class="upcoming-events">
                        <h2>Connect with Us</h2>
                        <p>Check out our calendar for upcoming events, workshops, and seminars. Don't miss the
                            opportunity to be part of the Techville experience.</p>
                    </div>
                </div>

                <aside class="sidebar" id="sidebar-right">
                    <div class="about-techville">
                        <h2>Who We Are</h2>
                        <p>Discover the rich history, mission, and values that define Techville University.</p>
                    </div>

                    <div class="university-leadership">
                        <h2>Meet Our Leaders</h2>
                        <p>Learn about the visionaries leading Techville University to new heights.</p>
                    </div>
                </aside>
            </div>

            <footer id="footerDefault">
                <div class="footer-links">
                    <a href="#">Privacy Policy</a> | <a href="#">Terms of Service</a>
                </div>
                <p>© 2023 Techville University. All rights reserved.</p>
            </footer>
        </form>
    </body>

    </html>