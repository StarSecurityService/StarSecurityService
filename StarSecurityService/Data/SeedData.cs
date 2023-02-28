using Microsoft.EntityFrameworkCore;
using StarSecurityService.Models;

namespace StarSecurityService.Data
{
    public static class SeedData
    {
        public static void initialize(IServiceProvider serviceprovider)
        {
            using (var context = new StarSecurityServiceDbContext(
            serviceprovider.GetRequiredService<
                DbContextOptions<StarSecurityServiceDbContext>>()))
            {
                if (context.Accounts.Any())
                {
                    return;
                }

                // Roles
                var roles = new Role[]
                {
                    new Role{RoleName = "Admin" },
                    new Role{RoleName = "Staff" },
                    new Role{RoleName = "Customer" }
                };
                foreach (var role in roles)
                {
                    context.Roles.Add(role);
                }
                context.SaveChanges();

                // Accounts
                var accounts = new Account[]
                {
                    new Account
                    {
                        FirstName = "Dinh",
                        LastName = "Quang Anh",
                        Email = "anhdqth2109005@fpt.edu.vn",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        Phone = "0395100761",
                        Address = "Ninh Binh",
                        CardId = "00764",
                        RoleId = roles.Single(role => role.RoleName == "Customer").RoleId
                    },
                    new Account
                    {
                        FirstName = "Luong",
                        LastName = "Viet Hoang",
                        Email = "hoanglv2109009@fpt.edu.vn",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        Phone = "0123456789",
                        Address = "Bo De Thu",
                        CardId = "6677028",
                        RoleId = roles.Single(role => role.RoleName == "Staff").RoleId
                    },
                    new Account
                    {
                        FirstName = "Nguyen",
                        LastName = "Ba Khanh",
                        Email = "khanhnb2109013@fpt.edu.vn",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        Phone = "0123456789",
                        Address = "Ha Noi",
                        CardId = "1234321",
                        RoleId = roles.Single(role => role.RoleName == "Admin").RoleId
                    }
                };
                foreach (var account in accounts)
                {
                    context.Add(account);
                }
                context.SaveChanges();

                // Service 
                var services = new Service[]
                {
                    new Service
                    {
                        ServiceName = "Office Security Service",
                        Description = "An office is a place where businesses, companies or a unit organization work. In the office contains a lot of important documents and is the place to work, receive guests, negotiate business with customers. Therefore, it is very important to check and monitor people entering and leaving and protecting property's safety. This has created a growing and increasingly popular office security service.",
                        Price = 100,
                        Image = "close-up-concentrated-businesspeople.jpg"
                    },
                    new Service
                    {
                        ServiceName = "Building Security Service",
                        Description = "The security service provided by STAR SECURITY SERVICE building is receiving a lot of attention from customers at the present time. Especially in big city areas, where there are a lot of high-rise buildings and offices. With the number of people working and living up to thousands of people, the building often generates many complicated problems. Read the article below to learn more about this service!",
                        Price = 75,
                        Image = "full-shot-people-wearing-suits-gym.jpg"
                    },
                    new Service
                    {
                        ServiceName = "Bank Security Service",
                        Description = "The security service provided by STAR SECURITY SERVICE building is receiving a lot of attention from customers at the present time. Especially in big city areas, where there are a lot of high-rise buildings and offices. With the number of people working and living up to thousands of people, the building often generates many complicated problems. Read the article below to learn more about this service!",
                        Price = 120,
                        Image = "joyful-business-people-taking-off-face-masks-while-walking-with-luggage-outdoors-from-hotel-office-buildings-front-view-business-trip-end-epidemic-concept.jpg"
                    },
                    new Service
                    {
                        ServiceName = "School Security Service",
                        Description = "School security services are very developed and are the school's top concern today. School security services will help maintain order as well as regular patrols and supervision.",
                        Price = 10,
                        Image = "multi-ethnic-business-team.jpg"
                    },
                    new Service
                    {
                        ServiceName = "Super Market Security Service",
                        Description = "The demand for shopping at supermarkets is becoming extremely developed. With an extremely large number of customers, the work of ensuring security and safety is always one of the problems that many supermarkets have to worry and think about. Supermarket security service is the most effective solution chosen today. Services to help maintain order, security, safety, regular patrol and supervision in the supermarket area. So which company's supermarket security service has the best quality and reputation today? Please refer to the information in the article below.",
                        Price = 25,
                        Image = "security-guard-frisking-passenger.jpg"
                    },
                    new Service
                    {
                        ServiceName = "Restaurant Security Service",
                        Description = "Restaurant security services are increasingly developing to meet the strong needs of society. The restaurant is a place where many people gather, so the security needs here must always be taken care of and guaranteed.",
                        Price = 15,
                        Image = "serious-coworkers-waiting-business-partners.jpg"
                    },
                    new Service
                    {
                        ServiceName = "Inn Security Service",
                        Description = "Apartment security services are receiving a lot of attention in recent times. STAR SECURITY SERVICE professional security company specializes in providing professional security services with many years of experience, we have been receiving absolute trust and appreciation from many customers.",
                        Price = 20,
                        Image = "serious-successful-business-colleagues-posing.jpg"
                    },
                    new Service
                    {
                        ServiceName = "Hospital Security Service",
                        Description = "STAR SECURITY SERVICE is a leading unit in the field of providing professional hospital security services with many years of experience in the profession. Professional hospital security services are used by many customers today. The following article of Bach Thang will help you understand more about this hospital security service.",
                        Price = 25,
                        Image = "successful-businessmen-with-ties.jpg"
                    },
                    new Service
                    {
                        ServiceName = "Event and festivals Security Service",
                        Description = "Events and festivals will often have a large number of participants and security issues always need to be focused. There have been many unexpected cases that happened, the main reason being the lax protection, affecting both event owners and participants. In this article, STAR SECURITY SERVICE will introduce to you detailed information about event security services. Let's find out through the article below!",
                        Price = 75,
                        Image = "successful-business-team.jpg"
                    },
                    new Service
                    {
                        ServiceName = "Hotel Security Service",
                        Description = "The hotel is the favorite place to stay by customers when they are on vacation, travel or on business. Every day, the number of customers entering and leaving the hotel is very large, so the need to protect the safety of people and property as well as ensure the occurrence of damage, fire and explosion problems is very important. Because of that understanding, the hotel security service was born. So which hotel security service has the best quality and reputation today? Please refer to the information in the article below.",
                        Price = 50,
                        Image = "successful-business-team-standing-together.jpg"
                    },
                    new Service
                    {
                        ServiceName = "Personal Security Service",
                        Description = "The Personal is the favorite place to stay by customers when they are on vacation, travel or on business. Every day, the number of customers entering and leaving the hotel is very large, so the need to protect the safety of people and property as well as ensure the occurrence of damage, fire and explosion problems is very important. Because of that understanding, the hotel security service was born. So which hotel security service has the best quality and reputation today? Please refer to the information in the article below.",
                        Price = 150,
                        Image = "team-businesswomen-standing-together-outdoors-giving-support-each-other-discussing-project-front-view-community-teamwork-concept.jpg"
                    },
                };
                foreach (var service in services)
                {
                    context.Add(service);
                }
                context.SaveChanges();
                // Guard
                var guards = new Guard[]
                {
                    // Office Security Service
                    new Guard
                    {
                        FirstName = "Donald",
                        LastName = "Trump",
                        ServiceId = services.Single(service => service.ServiceName == "Office Security Service").ServiceId,
                        Phone = "0395100761",
                        Height = "6.07ft",
                        Weight = "90kg",
                        Status = "Free",
                        Avatar = "default.png",
                        CardId = "DT123456789"
                    },
                    new Guard
                    {
                        FirstName = "Joe",
                        LastName = "Biden",
                        ServiceId = services.Single(service => service.ServiceName == "Office Security Service").ServiceId,
                        Phone = "0123123456",
                        Height = "6.2ft",
                        Weight = "90kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "JB123456789"
                    },
                    new Guard
                    {
                        FirstName = "Hillary",
                        LastName = "Clinton",
                        ServiceId = services.Single(service => service.ServiceName == "Office Security Service").ServiceId,
                        Phone = "0123456123",
                        Height = "5.7ft",
                        Weight = "65kg",
                        Status = "Free",
                        Avatar = "default.png",
                        CardId = "HC123456789"
                    },

                    // Building Security Service
                    new Guard
                    {
                        FirstName = "Volodymyr Oleksandrovych",
                        LastName = "Zelensky",
                        ServiceId = services.Single(service => service.ServiceName == "Building Security Service").ServiceId,
                        Phone = "0323847493",
                        Height = "6ft",
                        Weight = "85kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "VOZ123456789"
                    },
                    new Guard
                    {
                        FirstName = "Richard",
                        LastName = "Nixon",
                        ServiceId = services.Single(service => service.ServiceName == "Building Security Service").ServiceId,
                        Phone = "7462857476",
                        Height = "6.3ft",
                        Weight = "100kg",
                        Status = "Free",
                        Avatar = "default.png",
                        CardId = "RN123456789"
                    },
                    new Guard
                    {
                        FirstName = "Henry",
                        LastName = "Kissinger",
                        ServiceId = services.Single(service => service.ServiceName == "Building Security Service").ServiceId,
                        Phone = "8325756383",
                        Height = "6.3ft",
                        Weight = "100kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "HR123456789"
                    },

                    // Personal Security Service
                    new Guard
                    {
                        FirstName = "Tap",
                        LastName = "Can Binh",
                        ServiceId = services.Single(service => service.ServiceName == "Personal Security Service").ServiceId,
                        Phone = "6352999536",
                        Height = "5.9ft",
                        Weight = "70kg",
                        Status = "Free",
                        Avatar = "default.png",
                        CardId = "TCB123456789"
                    },
                    new Guard
                    {
                        FirstName = "Park",
                        LastName = "Chung-Hee",
                        ServiceId = services.Single(service => service.ServiceName == "Personal Security Service").ServiceId,
                        Phone = "7231888363",
                        Height = "5.9ft",
                        Weight = "70kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "PCH123456789"
                    },
                    new Guard
                    {
                        FirstName = "Christian",
                        LastName = "de Castries",
                        ServiceId = services.Single(service => service.ServiceName == "Personal Security Service").ServiceId,
                        Phone = "6473009274",
                        Height = "6.2ft",
                        Weight = "80kg",
                        Status = "Free",
                        Avatar = "default.png",
                        CardId = "CDC123456789"
                    },

                    // Bank Security Service
                    new Guard
                    {
                        FirstName = "Wiliam",
                        LastName = "Crumm",
                        ServiceId = services.Single(service => service.ServiceName == "Bank Security Service").ServiceId,
                        Phone = "7485637573",
                        Height = "6.1ft",
                        Weight = "83kg",
                        Status = "Free",
                        Avatar = "default.png",
                        CardId = "WC123456798"
                    },
                    new Guard
                    {
                        FirstName = "Bruno",
                        LastName = "Hochmuth",
                        ServiceId = services.Single(service => service.ServiceName == "Bank Security Service").ServiceId,
                        Phone = "7464827464",
                        Height = "6.2ft",
                        Weight = "86kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "BH123456798"
                    },
                    new Guard
                    {
                        FirstName = "Robert",
                        LastName = "Franky Worley",
                        ServiceId = services.Single(service => service.ServiceName == "Bank Security Service").ServiceId,
                        Phone = "7463837462",
                        Height = "6.4ft",
                        Weight = "95kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "RFW123456798"
                    },

                    // School Security Service
                    new Guard
                    {
                        FirstName = "Keith",
                        LastName = "L.Ware",
                        ServiceId = services.Single(service => service.ServiceName == "Bank Security Service").ServiceId,
                        Phone = "6484736463",
                        Height = "6.2ft",
                        Weight = "85kg",
                        Status = "Free",
                        Avatar = "default.png",
                        CardId = "KLW123456798"
                    },
                    new Guard
                    {
                        FirstName = "Charles",
                        LastName = "J.Girard",
                        ServiceId = services.Single(service => service.ServiceName == "Bank Security Service").ServiceId,
                        Phone = "7584757465",
                        Height = "6.3ft",
                        Weight = "83kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "CJG123456798"
                    },
                    new Guard
                    {
                        FirstName = "William",
                        LastName = "R.Bond",
                        ServiceId = services.Single(service => service.ServiceName == "Bank Security Service").ServiceId,
                        Phone = "9437377375",
                        Height = "6.1ft",
                        Weight = "83kg",
                        Status = "Free",
                        Avatar = "default.png",
                        CardId = "WRB123456798"
                    },

                    // Super Market Security Service
                    new Guard
                    {
                        FirstName = "John",
                        LastName = "A.Dillard",
                        ServiceId = services.Single(service => service.ServiceName == "Super Market Security Service").ServiceId,
                        Phone = "9475616375",
                        Height = "6.2ft",
                        Weight = "85kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "JAD123456798"
                    },
                    new Guard
                    {
                        FirstName = "Rembrandt",
                        LastName = "Robinson",
                        ServiceId = services.Single(service => service.ServiceName == "Super Market Security Service").ServiceId,
                        Phone = "9758264375",
                        Height = "6.4ft",
                        Weight = "100kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "RR123456798"
                    },
                    new Guard
                    {
                        FirstName = "Richard",
                        LastName = "J.Tallman",
                        ServiceId = services.Single(service => service.ServiceName == "Super Market Security Service").ServiceId,
                        Phone = "8585858585",
                        Height = "6.2ft",
                        Weight = "98kg",
                        Status = "Free",
                        Avatar = "default.png",
                        CardId = "RJT123456798"
                    },

                    // Restaurant Security Service
                    new Guard
                    {
                        FirstName = "Philippe",
                        LastName = "Leclerc",
                        ServiceId = services.Single(service => service.ServiceName == "Restaurant Security Service").ServiceId,
                        Phone = "764658363",
                        Height = "6.1ft",
                        Weight = "88kg",
                        Status = "Free",
                        Avatar = "default.png",
                        CardId = "PL123456798"
                    },
                    new Guard
                    {
                        FirstName = "Etienne",
                        LastName = "Valluy",
                        ServiceId = services.Single(service => service.ServiceName == "Restaurant Security Service").ServiceId,
                        Phone = "8576847583",
                        Height = "6.4ft",
                        Weight = "86kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "EV123456798"
                    },
                    new Guard
                    {
                        FirstName = "Delattre",
                        LastName = "De Tassigny",
                        ServiceId = services.Single(service => service.ServiceName == "Restaurant Security Service").ServiceId,
                        Phone = "8576847583",
                        Height = "6.2ft",
                        Weight = "89kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "DDT123456798"
                    },
                    
                    // Inn Security Service
                    new Guard
                    {
                        FirstName = "Lyndon",
                        LastName = "B.Johnson",
                        ServiceId = services.Single(service => service.ServiceName == "Inn Security Service").ServiceId,
                        Phone = "7485763636",
                        Height = "6.15ft",
                        Weight = "88kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "LBJ123456798"
                    },
                    new Guard
                    {
                        FirstName = "Ton",
                        LastName = "Trung Son",
                        ServiceId = services.Single(service => service.ServiceName == "Inn Security Service").ServiceId,
                        Phone = "7573682643",
                        Height = "5.9ft",
                        Weight = "85kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "TTS123456798"
                    },
                    new Guard
                    {
                        FirstName = "Tuong",
                        LastName = "Gioi Thach",
                        ServiceId = services.Single(service => service.ServiceName == "Inn Security Service").ServiceId,
                        Phone = "8476573889",
                        Height = "5.9ft",
                        Weight = "85kg",
                        Status = "Free",
                        Avatar = "default.png",
                        CardId = "TGT123456798"
                    },

                    // Hospital Security Service
                    new Guard
                    {
                        FirstName = "Bui",
                        LastName = "Xuan Huan",
                        ServiceId = services.Single(service => service.ServiceName == "Hospital Security Service").ServiceId,
                        Phone = "8475736433",
                        Height = "5.7ft",
                        Weight = "74kg",
                        Status = "Free",
                        Avatar = "default.png",
                        CardId = "BXH123456798"
                    },
                    new Guard
                    {
                        FirstName = "Tien",
                        LastName = "Bip",
                        ServiceId = services.Single(service => service.ServiceName == "Hospital Security Service").ServiceId,
                        Phone = "7495743443",
                        Height = "5.8ft",
                        Weight = "78kg",
                        Status = "Free",
                        Avatar = "default.png",
                        CardId = "TB123456798"
                    },
                    new Guard
                    {
                        FirstName = "Phu",
                        LastName = "Le",
                        ServiceId = services.Single(service => service.ServiceName == "Hospital Security Service").ServiceId,
                        Phone = "7464736463",
                        Height = "5.6ft",
                        Weight = "60kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "PL123456798"
                    },

                    // Event and festivals Security Service
                    new Guard
                    {
                        FirstName = "Kenny",
                        LastName = "Sang",
                        ServiceId = services.Single(service => service.ServiceName == "Event and festivals Security Service").ServiceId,
                        Phone = "7475847643",
                        Height = "5.8ft",
                        Weight = "68kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "KNS123456798"
                    },
                    new Guard
                    {
                        FirstName = "Pham",
                        LastName = "Thoai",
                        ServiceId = services.Single(service => service.ServiceName == "Event and festivals Security Service").ServiceId,
                        Phone = "7486836937",
                        Height = "5.8ft",
                        Weight = "62kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "PTMP123456798"
                    },
                    new Guard
                    {
                        FirstName = "Ngo",
                        LastName = "Ba Kha",
                        ServiceId = services.Single(service => service.ServiceName == "Event and festivals Security Service").ServiceId,
                        Phone = "7485847584",
                        Height = "5.6ft",
                        Weight = "58kg",
                        Status = "Free",
                        Avatar = "default.png",
                        CardId = "NBK123456798"
                    },

                    // Hotel Security Service
                    new Guard
                    {
                        FirstName = "Khanh",
                        LastName = "Sky",
                        ServiceId = services.Single(service => service.ServiceName == "Hotel Security Service").ServiceId,
                        Phone = "7458394957",
                        Height = "5.6ft",
                        Weight = "55kg",
                        Status = "Free",
                        Avatar = "default.png",
                        CardId = "KSK123456798"
                    },
                    new Guard
                    {
                        FirstName = "Quach",
                        LastName = "Dung",
                        ServiceId = services.Single(service => service.ServiceName == "Hotel Security Service").ServiceId,
                        Phone = "7458394957",
                        Height = "5.8ft",
                        Weight = "58kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "QD123456798"
                    },
                    new Guard
                    {
                        FirstName = "Duong",
                        LastName = "Nhue",
                        ServiceId = services.Single(service => service.ServiceName == "Hotel Security Service").ServiceId,
                        Phone = "2546254934",
                        Height = "6ft",
                        Weight = "62kg",
                        Status = "On duty",
                        Avatar = "default.png",
                        CardId = "DNDN123456798"
                    },
                };
                foreach (var guard in guards)
                {
                    context.Add(guard);
                }
                context.SaveChanges();
            }
        }
    }
}
