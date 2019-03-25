using IdentityModel;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Configuration
{
    public class Users
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser{SubjectId = "818727", Username = "Michael", Password = "password",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Michael Boeynaems"),
                        new Claim(JwtClaimTypes.GivenName, "Michael"),
                        new Claim(JwtClaimTypes.FamilyName, "Boeynaems"),
                        new Claim(JwtClaimTypes.Email, "michael.boeynaems@portasecura.com"),
                        new Claim(JwtClaimTypes.Role, "Reviewer"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://www.portasecura.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 2450, 'country': 'Belgium' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                new TestUser{SubjectId = "7265", Username = "Johan", Password = "password",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Johan Peeters"),
                        new Claim(JwtClaimTypes.GivenName, "Johan"),
                        new Claim(JwtClaimTypes.FamilyName, "Peeters"),
                        new Claim(JwtClaimTypes.Email, "yo@johanpeeters.com"),
                        new Claim(JwtClaimTypes.Role, "Admin"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://www.johanpeeters.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'Two Hacker Way', 'locality': 'Heidelberg', 'postal_code': 3000, 'country': 'Belgium' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("location", "somewhere"),
                    }
                 },
                 new TestUser{SubjectId = "50001", Username = "student1", Password = "Student",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Student 1"),
                        new Claim(JwtClaimTypes.GivenName, "Student"),
                        new Claim(JwtClaimTypes.FamilyName, "1"),
                        new Claim(JwtClaimTypes.Email, "student1@example.org"),
                        new Claim(JwtClaimTypes.Role, "Reviewer"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://www.example.org"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 2450, 'country': 'Belgium' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                 new TestUser{SubjectId = "50002", Username = "student2", Password = "Student",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Student 2"),
                        new Claim(JwtClaimTypes.GivenName, "Student"),
                        new Claim(JwtClaimTypes.FamilyName, ""),
                        new Claim(JwtClaimTypes.Email, "student2@example.org"),
                        new Claim(JwtClaimTypes.Role, "Reviewer"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://www.example.org"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 2450, 'country': 'Belgium' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                 new TestUser{SubjectId = "50003", Username = "student3", Password = "Student",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Student 3"),
                        new Claim(JwtClaimTypes.GivenName, "Student"),
                        new Claim(JwtClaimTypes.FamilyName, "3"),
                        new Claim(JwtClaimTypes.Email, "student3@example.org"),
                        new Claim(JwtClaimTypes.Role, "Reviewer"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://www.example.org"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 2450, 'country': 'Belgium' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                 new TestUser{SubjectId = "50004", Username = "student4", Password = "Student",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Student 4"),
                        new Claim(JwtClaimTypes.GivenName, "Student"),
                        new Claim(JwtClaimTypes.FamilyName, "4"),
                        new Claim(JwtClaimTypes.Email, "student4@example.org"),
                        new Claim(JwtClaimTypes.Role, "Reviewer"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://www.example.org"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 2450, 'country': 'Belgium' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                 new TestUser{SubjectId = "50005", Username = "student5", Password = "Student",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Student 5"),
                        new Claim(JwtClaimTypes.GivenName, "Student"),
                        new Claim(JwtClaimTypes.FamilyName, "5"),
                        new Claim(JwtClaimTypes.Email, "student5@example.org"),
                        new Claim(JwtClaimTypes.Role, "Reviewer"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://www.example.org"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 2450, 'country': 'Belgium' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                 new TestUser{SubjectId = "50006", Username = "student6", Password = "Student",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Student 6"),
                        new Claim(JwtClaimTypes.GivenName, "Student"),
                        new Claim(JwtClaimTypes.FamilyName, "6"),
                        new Claim(JwtClaimTypes.Email, "student6@example.org"),
                        new Claim(JwtClaimTypes.Role, "Reviewer"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://www.example.org"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 2450, 'country': 'Belgium' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                 new TestUser{SubjectId = "50007", Username = "student7", Password = "Student",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Student 7"),
                        new Claim(JwtClaimTypes.GivenName, "Student"),
                        new Claim(JwtClaimTypes.FamilyName, "7"),
                        new Claim(JwtClaimTypes.Email, "student7@example.org"),
                        new Claim(JwtClaimTypes.Role, "Reviewer"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://www.example.org"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 2450, 'country': 'Belgium' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                 new TestUser{SubjectId = "50008", Username = "student8", Password = "Student",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Student 8"),
                        new Claim(JwtClaimTypes.GivenName, "Student"),
                        new Claim(JwtClaimTypes.FamilyName, "8"),
                        new Claim(JwtClaimTypes.Email, "student8@example.org"),
                        new Claim(JwtClaimTypes.Role, "Reviewer"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://www.example.org"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 2450, 'country': 'Belgium' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                 new TestUser{SubjectId = "50009", Username = "student9", Password = "Student",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Student 9"),
                        new Claim(JwtClaimTypes.GivenName, "Student"),
                        new Claim(JwtClaimTypes.FamilyName, "9"),
                        new Claim(JwtClaimTypes.Email, "student10@example.org"),
                        new Claim(JwtClaimTypes.Role, "Reviewer"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://www.example.org"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 2450, 'country': 'Belgium' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                 new TestUser{SubjectId = "500010", Username = "student10", Password = "Student",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Student 10"),
                        new Claim(JwtClaimTypes.GivenName, "Student"),
                        new Claim(JwtClaimTypes.FamilyName, "10"),
                        new Claim(JwtClaimTypes.Email, "student10@example.org"),
                        new Claim(JwtClaimTypes.Role, "Reviewer"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://www.example.org"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 2450, 'country': 'Belgium' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                 new TestUser{SubjectId = "500011", Username = "student11", Password = "Student",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Student 11"),
                        new Claim(JwtClaimTypes.GivenName, "Student"),
                        new Claim(JwtClaimTypes.FamilyName, "11"),
                        new Claim(JwtClaimTypes.Email, "student11@example.org"),
                        new Claim(JwtClaimTypes.Role, "Reviewer"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://www.example.org"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 2450, 'country': 'Belgium' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                 new TestUser{SubjectId = "500012", Username = "student12", Password = "Student",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Student 12"),
                        new Claim(JwtClaimTypes.GivenName, "Student"),
                        new Claim(JwtClaimTypes.FamilyName, "12"),
                        new Claim(JwtClaimTypes.Email, "student12@example.org"),
                        new Claim(JwtClaimTypes.Role, "Reviewer"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://www.example.org"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 2450, 'country': 'Belgium' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
            };
        }
    }
}
