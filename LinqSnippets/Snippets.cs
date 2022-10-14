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

        // TODO:

        // Variables

        // ZIP

        // Repeat

        // ALL

        // Aggregate

        // Distinct

        // GroupBy
    }
}