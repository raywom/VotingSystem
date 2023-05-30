using Domain;
using Microsoft.AspNetCore.Identity;
// TODO: Add more using statements, kenkeles
// TODO: Add more other fields, kenkeles
namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context,
            UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any() && !context.Polls.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com",
                        PasswordHash = "Samsung"
                    },
                    new AppUser
                    {
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com"
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                var choices = new List<Choice>
                {
                    new Choice
                    {
                        Title = "Choice 1"
                    },
                    new Choice
                    {
                        Title = "Choice 2"
                    },
                    new Choice
                    {
                        Title = "Choice 3"
                    },
                };

                var activities = new List<Poll>
                {
                    new Poll
                    {
                        Title = "Future Activity 1",
                        CloseDate = DateTime.UtcNow.AddMonths(2),
                        Description = "Activity 1",
                        Category = "drinks",
                        Choices = choices,
                        Voters = new List<Vote>
                        {
                            new Vote
                            {
                                AppUser = users[0],
                                IsHost = true,
                                Choice = choices[0]
                            },
                            new Vote
                            {
                                AppUser = users[1],
                                IsHost = false,
                                Choice = choices[1]
                            },
                            new Vote
                            {
                                AppUser = users[2],
                                IsHost = false,
                                Choice = choices[2]
                            },
                        }
                    },
                //     new Poll
                //     {
                //         Title = "Past Activity 2",
                //         CloseDate = DateTime.UtcNow.AddMonths(-1),
                //         Description = "Activity 1 month ago",
                //         Category = "culture",
                //         Voters = new List<Vote>
                //         {
                //             new Vote
                //             {
                //                 AppUser = users[0],
                //                 IsHost = true
                //             },
                //             new Vote
                //             {
                //                 AppUser = users[1],
                //                 IsHost = false
                //             },
                //         }
                //     },
                //     new Poll
                //     {
                //         Title = "Future Activity 1",
                //         CloseDate = DateTime.UtcNow.AddMonths(1),
                //         Description = "Activity 1 month in future",
                //         Category = "music",
                //         Voters = new List<Vote>
                //         {
                //             new Vote
                //             {
                //                 AppUser = users[2],
                //                 IsHost = true
                //             },
                //             new Vote
                //             {
                //                 AppUser = users[1],
                //                 IsHost = false
                //             },
                //         }
                //     },
                //     new Poll
                //     {
                //         Title = "Future Activity 2",
                //         CloseDate = DateTime.UtcNow.AddMonths(2),
                //         Description = "Activity 2 months in future",
                //         Category = "food",
                //         Voters = new List<Vote>
                //         {
                //             new Vote
                //             {
                //                 AppUser = users[0],
                //                 IsHost = true
                //             },
                //             new Vote
                //             {
                //                 AppUser = users[2],
                //                 IsHost = false
                //             },
                //         }
                //     },
                //     new Poll
                //     {
                //         Title = "Future Activity 3",
                //         CloseDate = DateTime.UtcNow.AddMonths(3),
                //         Description = "Activity 3 months in future",
                //         Category = "drinks",
                //         Voters = new List<Vote>
                //         {
                //             new Vote
                //             {
                //                 AppUser = users[1],
                //                 IsHost = true                            
                //             },
                //             new Vote
                //             {
                //                 AppUser = users[0],
                //                 IsHost = false                            
                //             },
                //         }
                //     },
                //     new Poll
                //     {
                //         Title = "Future Activity 4",
                //         CloseDate = DateTime.UtcNow.AddMonths(4),
                //         Description = "Activity 4 months in future",
                //         Category = "culture",
                //         Voters = new List<Vote>
                //         {
                //             new Vote
                //             {
                //                 AppUser = users[1],
                //                 IsHost = true                            
                //             }
                //         }
                //     },
                //     new Poll
                //     {
                //         Title = "Future Activity 5",
                //         CloseDate = DateTime.UtcNow.AddMonths(5),
                //         Description = "Activity 5 months in future",
                //         Category = "drinks",
                //         Voters = new List<Vote>
                //         {
                //             new Vote
                //             {
                //                 AppUser = users[0],
                //                 IsHost = true                            
                //             },
                //             new Vote
                //             {
                //                 AppUser = users[1],
                //                 IsHost = false                            
                //             },
                //         }
                //     },
                //     new Poll
                //     {
                //         Title = "Future Activity 6",
                //         CloseDate = DateTime.UtcNow.AddMonths(6),
                //         Description = "Activity 6 months in future",
                //         Category = "music",
                //         Voters = new List<Vote>
                //         {
                //             new Vote
                //             {
                //                 AppUser = users[2],
                //                 IsHost = true                            
                //             },
                //             new Vote
                //             {
                //                 AppUser = users[1],
                //                 IsHost = false                            
                //             },
                //         }
                //     },
                //     new Poll
                //     {
                //         Title = "Future Activity 7",
                //         CloseDate = DateTime.UtcNow.AddMonths(7),
                //         Description = "Activity 7 months in future",
                //         Category = "travel",
                //         Voters = new List<Vote>
                //         {
                //             new Vote
                //             {
                //                 AppUser = users[0],
                //                 IsHost = true                            
                //             },
                //             new Vote
                //             {
                //                 AppUser = users[2],
                //                 IsHost = false                            
                //             },
                //         }
                //     },
                //     new Poll
                //     {
                //         Title = "Future Activity 8",
                //         CloseDate = DateTime.UtcNow.AddMonths(8),
                //         Description = "Activity 8 months in future",
                //         Category = "drinks",
                //         Voters = new List<Vote>
                //         {
                //             new Vote
                //             {
                //                 AppUser = users[2],
                //                 IsHost = true                            
                //             },
                //             new Vote
                //             {
                //                 AppUser = users[1],
                //                 IsHost = false                            
                //             },
                //         }
                //     }
                };

                await context.Polls.AddRangeAsync(activities);
                await context.SaveChangesAsync();
            }
        }
    }
}
