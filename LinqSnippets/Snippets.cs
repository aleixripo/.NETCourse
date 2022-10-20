using System.Diagnostics;
using System.Xml.Linq;

namespace LinqSnippets
{
    public class Snippets
    {
        public static void BasicLinQ()
        {
            string[] cars =
            {
                "VM Golf",
                "VM California",
                "Audi A3",
                "Fiat Puno",
                "Seat Ibiza",
                "Seat Leon"
            };


            // 1. SELECT * of cars
            var carList = from car in cars select car;

            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }

            // 2. SELECT WHERE car is Audi
            var audiList = from car in cars where car.Contains("Audi") select car;

            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }

        }

        // Number Examples
        public static void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Each Number multiplied by 3
            // take all numbers, but 9
            // Order numbers by ascending value

            var processedNumberList = numbers
                .Select(num => num * 3) // {3, 6, 9, etc...}
                .Where(num => num != 9) // {all but the 9}
                .OrderBy(num => num); // at the end, we order ascending
        }

        public static void SearchExamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c",
            };

            // 1. Firt of all elements
            var first = textList.First();

            // 2. First elemetn that is "c"
            var cText = textList.First(text => text == "c");

            // 3. First elemetn that contains "j"
            var jText = textList.First(text => text.Contains("j"));

            // 4. First element that contains Z or default
            var firstOfDefault = textList.FirstOrDefault(text => text.Contains("z")); // "" or element that containz "z"

            // 4. Last element that contains Z or default
            var lastOfDefault = textList.LastOrDefault(text => text.Contains("z")); // "" or last element that containz "z"

            // 5. Single Values
            var uniqueTexts = textList.Single();
            var uniqueOrDefaultTexts = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            // Obtain {4, 8 }
            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers); // { 4, 8, }
        }


        public static void MultipleSelects()
        {
            // SELECT MANY
            string[] myOpinions =
            {
                "Opinion 1, text 1",
                "Opinion 2, text 2",
                "Opinion 3, text 3"
            };

            var myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id=1,
                            Name="Aleix",
                            Email= "aleix.ripoll.ext@inetum.com",
                            Salary = 3000
                        },
                        new Employee()
                        {
                            Id=2,
                            Name="Pepe",
                            Email= "pepe.ext@inetum.com",
                            Salary = 2000
                        },
                        new Employee()
                        {
                            Id=3,
                            Name="Juanjo",
                            Email= "juanjo.ext@inetum.com",
                            Salary = 1000
                        }
                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id=4,
                            Name="Sabrina",
                            Email= "sabrina.ext@inetum.com",
                            Salary = 3000
                        },
                        new Employee()
                        {
                            Id=5,
                            Name="Joshua",
                            Email= "joshua.ext@inetum.com",
                            Salary = 2000
                        },
                        new Employee()
                        {
                            Id=6,
                            Name="Mireia",
                            Email= "mireia.ext@inetum.com",
                            Salary = 1000
                        }
                    }
                }
            };

            // Obtain all Employyes of all Enterprises
            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            // Know if any list is empty
            bool hasEnterprises = enterprises.Any();
            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            // All enterprises at least employee with at least 1000€ of salary
            bool hasEmployeeWithSalaryMoreThannOrEqual1000 =
                enterprises.Any(enterprise =>
                    enterprise.Employees.Any(employee => employee.Salary >= 1000));
        }

        static public void linqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "c", "d" };


            // INNNER JOIN
            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(
                secondList,
                element => element,
                secondElement => secondElement,
                (element, secondElement) => new { element, secondElement });

            // OUTER JOIN - LEFT
            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };

            var leftOuterJoin2 = from element in firstList
                                 from seconElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = seconElement };

            // OUTER JOIN - RIGHT
            var rightOuterJoin = from secondElement in firstList
                                 join element in secondList
                                 on secondElement equals element
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where secondElement != temporalElement
                                 select new { Element = secondElement };

            // UNION
            var unionList = leftOuterJoin.Union(rightOuterJoin);
        }

        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };

            // SKIP

            var skipTwoFirstValues = myList.Skip(2); // { 3,4,5,6,7,8,9,10 }

            var skipLastTwoVallues = myList.SkipLast(2); // { 1,2,3,4,5,6,7,8 }

            var skipWhile = myList.SkipWhile(num => num < 4); // { 4,5,6,7,8 }

            // TAKE

            var takeFirstTwoValues = myList.Take(2); // { 1,2 } 

            var takeLastTwoValues = myList.TakeLast(2); // { 9,10 }

            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4); // { 1,2,3 }
        }

        // Paging with Skip & Take
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            int startIndex = (pageNumber = 1) + resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage);
        }

        // Variables
        static public void LinqVariables()
        {
            int[] numbers = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquared = Math.Pow(number, 2)
                               where nSquared > average
                               select number;

            Console.WriteLine("Average: {0}", numbers.Average());

            foreach (int number in aboveAverage)
            {
                Console.WriteLine("Number: {0} Square: {1} ", number, Math.Pow(number, 2));
            }
        }

        // ZIP
        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = {"one", "two", "three", "four", "five"};

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + " = " + word);

            // {"1 = one", "2 = two", ...}
        }


        // Repeat & Range
        static public void repeatRangeLinq()
        {
            // Generate collection from 1 - 1000 ---> Range
            IEnumerable<int> first1000 = Enumerable.Range(1, 1000);

            // Repeat a value N times
            IEnumerable<string> fiveXx = Enumerable.Repeat("X", 5); // {"X" * 5}
        }

        static public void studentsLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Aleix",
                    Grade = 90,
                    Certified = true
                },
                new Student
                {
                    Id = 2,
                    Name = "Romero",
                    Grade = 50,
                    Certified = false
                },
                new Student
                {
                    Id = 3,
                    Name = "Marco",
                    Grade = 100,
                    Certified = true
                },
                new Student
                {
                    Id = 4,
                    Name = "Joshua",
                    Grade = 20,
                    Certified = false
                },
                new Student
                {
                    Id = 5,
                    Name = "Martín",
                    Grade = 70,
                    Certified = true
                }
            };

            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student.Name;

            var notCertified = from student in classRoom
                               where student.Certified == false
                               select student.Name;

            var approvedStudents = from student in classRoom
                                   where student.Grade >= 50 && student.Certified == true
                                   select student.Name;

        }

        // ALL
        static public void AllLinq()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };

            bool allAreSmallerThan10 = numbers.All(x => x < 10); // true
            bool allAreBiggerOrEqualThan2 = numbers.All(x => x >= 2); // false

            var emptyLIst = new List<int>();
            bool allNumbersAreGreaterThan0 = numbers.All(x => x >= 0); // true
        }

        // Aggregate
        static public void aggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            // Sum all numbers
            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);

            // 0, 1 => 1
            // 1, 2 => 3
            // 3, 4 => 7

            string[] words = { "hello", "my", "name", "is", "Aleix" }; // hello my name is Aleix
            string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + current);
        }

        // Distinct
        static public void distinctValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
            IEnumerable<int> distincValues = numbers.Distinct();
        }

        // GroupBy
        static public void groupByExample()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Obtain only even numbers and generate two grups
            var grouped = numbers.GroupBy(x => x % 2 == 0);

            // We will have two groups
            // 1. The group that doesnt fit the condition (odd numbers)
            // 2. Th group that fiits the condition (even numbers)


            foreach (var group in grouped)
            {
                foreach (var value in group)
                {
                    Console.WriteLine(value); // 1,3,5,7,9 ... 2,4,6,8 (firs the odds and then the even)
                }
            }

            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Aleix",
                    Grade = 90,
                    Certified = true
                },
                new Student
                {
                    Id = 2,
                    Name = "Romero",
                    Grade = 50,
                    Certified = false
                },
                new Student
                {
                    Id = 3,
                    Name = "Marco",
                    Grade = 100,
                    Certified = true
                },
                new Student
                {
                    Id = 4,
                    Name = "Joshua",
                    Grade = 20,
                    Certified = false
                },
                new Student
                {
                    Id = 5,
                    Name = "Martín",
                    Grade = 70,
                    Certified = true
                }
            };

            var certidiedQuery = classRoom.GroupBy(student => student.Certified && student.Grade >= 50);

            // We obtain two groups
            // 1. Not certified students
            // 2. Certified students

            foreach (var group in certidiedQuery)
            {
                Console.WriteLine("------- {0}------", group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine(student.Name);
                }
            }
        }

        static public void relationsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 1,
                            Created = DateTime.Now,
                            Title = "My first comment",
                            Content = "My contnet"
                        },
                        new Comment()
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title = "My second comment",
                            Content = "My contnet"
                        },
                        new Comment()
                        {
                            Id = 3,
                            Created = DateTime.Now,
                            Title = "My first comment",
                            Content = "My other contnet"
                        }
                    }
                },
                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "My first content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 3,
                            Created = DateTime.Now,
                            Title = "My other comment",
                            Content = "My new contnet"
                        },
                        new Comment()
                        {
                            Id = 4,
                            Created = DateTime.Now,
                            Title = "My other new comment",
                            Content = "My new contnet"
                        },
                        new Comment()
                        {
                            Id = 5,
                            Created = DateTime.Now,
                            Title = "My other ohter new comment",
                            Content = "My other new contnet"
                        }
                    }
                }
            };

            var commentsContent = posts.SelectMany(
                post => post.Comments, 
                (post , comment) => new { PostID = post.Id, CommentContent = comment.Content});




        }
    }
}