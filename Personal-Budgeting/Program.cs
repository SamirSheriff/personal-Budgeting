namespace Personal_Budgeting
{
    internal static class Program
    {
        static User loadeduser;
        static void Main()
        {
            Console.WriteLine("=====================================");
            Console.WriteLine("         Welcome to Numo");
            Console.WriteLine("=====================================");

            Console.WriteLine("Menu:");
            Console.WriteLine("1) Sign-up");
            Console.WriteLine("2) Login");
            Console.Write("Enter your choice (1, 2): ");

            string choice = Console.ReadLine();

            while(choice != "1" && choice != "2"){
                Console.WriteLine("Error, this choice is not found.\nTry again");
                Console.Write("You must write 1 or 2: ");
                choice = Console.ReadLine();
            }

            if(choice == "1"){
                Signup();
            }

            else{
                Login();
            }

            homePage();
        }

        static void Signup(){
            string username, email, password;
                AuthManager authManager = new AuthManager();

                Console.WriteLine("=====================================");
                Console.WriteLine("               Sign-up");
                Console.WriteLine("=====================================");

                Console.Write("Enter a username: ");
                username = Console.ReadLine();
                Console.Write("Enter an email: ");
                email = Console.ReadLine();
                Console.Write("Enter a password: ");
                password = Console.ReadLine();

                // Validation for strong password
                while(password.Length < 8){
                    Console.WriteLine("This password is too weak");
                    Console.WriteLine("the password must be at least 8 characters");
                    Console.Write("Enter a stronger password: ");
                    password = Console.ReadLine();
                }

                authManager.signup(username, email, password);
                loadeduser = authManager.login(username, password);
        }

        static void Login(){
            string username, password;
            AuthManager authManager = new AuthManager();

            Console.WriteLine("=====================================");
            Console.WriteLine("               Login");
            Console.WriteLine("=====================================");

            Console.Write("Enter the username: ");
            username = Console.ReadLine();

            Console.Write("Enter the password: ");
            password = Console.ReadLine();

            loadeduser = authManager.login(username, password);
        }

        static void homePage(){
            Console.WriteLine("=====================================");
            Console.WriteLine("               Home Page");
            Console.WriteLine("=====================================");

            Console.WriteLine(new string('-', 133));
            Console.WriteLine(" 1) Incomes Page       |       2) Budgeting Page        |       3) Expenses Page      |       4) Reminders Page      |       5) Exit");
            Console.WriteLine(new string('-', 133));

            Console.WriteLine("Enter the page number that you want to visit: ");
            string choice = Console.ReadLine();

            while(choice != "1" && choice != "2" && choice != "3" && choice != "4")
            {
                Console.WriteLine("Please, Enter a vaild choice (1, 2, 3 or 4): ");
                choice = Console.ReadLine();
            }

            if(choice == "1")
            {
                incomesPage();
            }
            else if(choice == "2")
            {
                budgetingPage();
            }
            else if(choice == "3")
            {
                expensesPage();
            }
            else if(choice == "4")
            {
                remainderPage();
            }
            else
            {
                Environment.Exit(0);
            }

            homePage();
        }

        static void incomesPage(){
            string choice;

            Console.WriteLine("=====================================");
            Console.WriteLine("               Incomes");
            Console.WriteLine("=====================================");

            if(loadeduser.incomeManager.incomes.Count > 0)
            {
                Console.WriteLine("--------  --------  ----------------  --------------");

                Console.WriteLine(
                $"{ "ID".PadRight(8) }  " +
                $"{ "Amount".PadLeft(8) }  " +
                $"{ "Recieve Date".PadRight(16) }  " +
                $"{ "Nonrecurring".PadRight(14) }");

                Console.WriteLine("--------  --------  ----------------  --------------");

                foreach (var income in loadeduser.incomeManager.incomes)
                {
                    Console.WriteLine(
                    $"{ income.id.ToString().PadRight(8) }  " +
                    $"{ income.amount.ToString().PadLeft(8) }  " +
                    $"{ income.recieveDate.ToString().Split(' ')[0].PadRight(16) }  " +
                    $"{ income.isNonRecurring.ToString().PadRight(14) }");
                }

                Console.WriteLine(new string('-', 52));

                Console.WriteLine("\n");
                Console.WriteLine(new string('-', 77));
                Console.WriteLine(" 1) Add Income        |       2) Edit Inocme      |       3) Delete Income ");
                Console.WriteLine(new string('-', 77));
                Console.Write("Enter you choice (1, 2, 3): ");
                choice = Console.ReadLine();

                while(choice != "1" && choice != "2" && choice != "3")
                {
                    Console.WriteLine("Please, Enter a vaild choice (1, 2 or 3): ");
                    choice = Console.ReadLine();
                }

                if(choice == "1")
                {
                    addIncome();
                }

                else if(choice == "2")
                {
                    editIncome();
                }
                else
                {
                    removeIncome();
                }
            }
            else
            {
                Console.WriteLine("Add Income: ");
                addIncome();
            }

        }

        static void addIncome(){
            double amount;
            DateTime recieveDate;
            bool isNonRecurring;

            Console.Write("Enter the income amount: ");
            while (!double.TryParse(Console.ReadLine(), out amount))
            {
                Console.Write("Invalid input. Please enter a valid number for amount: ");
            }


            Console.WriteLine("Enter the recieve Date (e.g. yyyy-MM-dd): ");
            while (!DateTime.TryParse(Console.ReadLine(), out recieveDate))
            {
                Console.Write("Invalid input. Please enter a valid date (e.g. yyyy-MM-dd): ");
            }

            Console.Write("Is this income NonRecurring ? (true/false): ");
            while (!bool.TryParse(Console.ReadLine(), out isNonRecurring))
            {
                Console.Write("Invalid input. Please enter 'true' or 'false': ");
            }

            loadeduser.incomeManager.addRecord(amount, recieveDate, isNonRecurring);
        }

        static void editIncome(){
            int incomeId;
            double amount;
            DateTime recieveDate;
            bool isNonRecurring;

            Console.Write("Enter the income id that you want it change: ");
            while (!int.TryParse(Console.ReadLine(), out incomeId))
            {
                Console.Write("Invalid input. Please enter a valid integer for income id: ");
            }
            
            Console.Write("Enter the amount of this income: ");
            while (!double.TryParse(Console.ReadLine(), out amount))
            {
                Console.Write("Invalid input. Please enter a valid number for amount: ");
            }


            Console.WriteLine("Enter the recieve Date (e.g. yyyy-MM-dd): ");
            while (!DateTime.TryParse(Console.ReadLine(), out recieveDate))
            {
                Console.Write("Invalid input. Please enter a valid date (e.g. yyyy-MM-dd): ");
            }

            Console.Write("Is this income NonRecurring ? (true/false): ");
            while (!bool.TryParse(Console.ReadLine(), out isNonRecurring))
            {
                Console.Write("Invalid input. Please enter 'true' or 'false': ");
            }

            loadeduser.incomeManager.editRecord(incomeId, amount, recieveDate, isNonRecurring);
        }

        static void removeIncome(){
            int id;

            Console.Write("Enter the income id: ");
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Invalid input. Please enter a valid integer for id: ");
            }

            loadeduser.incomeManager.deleteRecord(id);
        }

        static void expensesPage(){        
            Console.WriteLine("=====================================");
            Console.WriteLine("               Expenses");
            Console.WriteLine("=====================================");

            if (loadeduser.expenseManager.expenses.Count > 0)
            {
                Console.WriteLine("--------  --------  --------------------  ------------");
                Console.WriteLine(
                $"{ "ID".PadRight(8) }  " +
                $"{ "Amount".PadLeft(8) }  " +
                $"{ "category".PadRight(20) }  " +
                $"{ "Recurring".PadLeft(12) }");

                Console.WriteLine("--------  --------  --------------------  ------------");                

                foreach (var expense in loadeduser.expenseManager.expenses){
                    Console.WriteLine(
                    $"{ expense.id.ToString().PadRight(8) }  " +
                    $"{ expense.amount.ToString().PadLeft(8) }  " +
                    $"{ expense.category.PadRight(20) }  " +
                    $"{ expense.isRecurring.ToString().PadLeft(12) }");
                }

                Console.WriteLine(new string('-', 54));

                Console.WriteLine("\n");
                Console.WriteLine(new string('-', 80));
                Console.WriteLine(" 1) Add expense        |       2) Edit expense      |       3) Delete expense ");
                Console.WriteLine(new string('-', 80));
                Console.Write("Enter you choice (1, 2, 3): ");
                string choice = Console.ReadLine();

                while(choice != "1" && choice != "2" && choice != "3")
                {
                    Console.WriteLine("Please, Enter a vaild choice (1, 2 or 3): ");
                    choice = Console.ReadLine();
                }

                if(choice == "1")
                {
                    addExpense();
                }

                else if(choice == "2")
                {
                    editExpense();
                }
                else
                {
                    removeExpense();
                }
            }
            else
            {
                Console.WriteLine("Add expense:");
                addExpense();
            }
        }

        static void addExpense(){
            string category;
            double amount;
            bool isRecurring;

            Console.Write("Enter the expense amount: ");
            while (!double.TryParse(Console.ReadLine(), out amount))
            {
                Console.Write("Invalid input. Please enter a valid number for amount: ");
            }

            Console.Write("Enter the category of expense: ");
            category = Console.ReadLine();

            Console.Write("Is this expense Recurring ? (true/false): ");
            while (!bool.TryParse(Console.ReadLine(), out isRecurring))
            {
                Console.Write("Invalid input. Please enter 'true' or 'false': ");
            }

            loadeduser.expenseManager.addRecord(category, amount, isRecurring);
        }

        static void editExpense(){
            int expenseId;
            string category;
            double amount;
            bool isRecurring;

            Console.Write("Enter the expense id that you want it change: ");
            while (!int.TryParse(Console.ReadLine(), out expenseId))
            {
                Console.Write("Invalid input. Please enter a valid integer for income id: ");
            }
            
            Console.Write("Enter the amount of this expense: ");
            while (!double.TryParse(Console.ReadLine(), out amount))
            {
                Console.Write("Invalid input. Please enter a valid number for amount: ");
            }
            
            Console.Write("Enter the category: ");
            category = Console.ReadLine();

            Console.Write("Is this expense Recurring ? (true/false): ");
            while (!bool.TryParse(Console.ReadLine(), out isRecurring))
            {
                Console.Write("Invalid input. Please enter 'true' or 'false': ");
            }

            loadeduser.expenseManager.editRecord(expenseId, category, amount, isRecurring);
        }

        static void removeExpense(){
            int id;

            Console.Write("Enter the expense id: ");
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Invalid input. Please enter a valid integer for id: ");
            }

            loadeduser.expenseManager.deleteRecord(id);
        }

        static void budgetingPage(){
            Console.WriteLine("=====================================");
            Console.WriteLine("               Budgeting");
            Console.WriteLine("=====================================");

            if (loadeduser.budgetManager.budgets.Count > 0)
            {
                Console.WriteLine("--------  ----------  ------------------  --------------------");

                Console.WriteLine(
                $"{ "ID".PadRight(8) }  " +
                $"{ "Limit".PadRight(10) }  " +
                $"{ "Spending Amount".PadRight(18) }  " +
                $"{ "category".PadRight(20) }");

                Console.WriteLine("--------  ----------  ------------------  --------------------");

                foreach (var budget in loadeduser.budgetManager.budgets){
                    Console.WriteLine(
                    $"{ budget.id.ToString().PadRight(8) }  " +
                    $"{ budget.limit.ToString().PadRight(10) }  " +
                    $"{ budget.amountSpent.ToString().PadRight(18) }  " +
                    $"{ budget.category.PadRight(20) }");
                }

                Console.WriteLine(new string('-', 54));

                //Console.WriteLine("What do you wnat to do ?");
                Console.WriteLine(new string('-', 75));
                Console.WriteLine(" 1) Add budget        |       2) Edit budget      |       3) Delete budget ");
                Console.WriteLine(new string('-', 75));
                Console.Write("Enter your choice (1, 2, 3): ");
                string choice = Console.ReadLine();

                while(choice != "1" && choice != "2" && choice != "3")
                {
                    Console.WriteLine("Please, Enter a vaild choice (1, 2 or 3): ");
                    choice = Console.ReadLine();
                }

                if(choice == "1")
                {
                    addBudget();
                }

                else if(choice == "2")
                {
                    editBudget();
                }
                else
                {
                    removeBudget();
                }
            }
            else 
            {
                Console.WriteLine("Add budget:");
                addBudget();
            }
        }

        static void addBudget(){
            double limit;
            double amountSpent;
            string category;

            Console.Write("Enter the budget limit: ");
            while (!double.TryParse(Console.ReadLine(), out limit))
            {
                Console.Write("Invalid input. Please enter a valid number for budget limit: ");
            }

            Console.Write("Enter the Amount of spending: ");
            while (!double.TryParse(Console.ReadLine(), out amountSpent))
            {
                Console.Write("Invalid input. Please enter a valid number for amount: ");
            }

            Console.Write("Enter the category: ");
            category = Console.ReadLine();

            loadeduser.budgetManager.addRecord(limit, amountSpent, category);
        }

        static void editBudget(){
            int id;
            double limit;
            double amountSpent;
            string category;

            Console.Write("Enter the budget id that you want it change: ");
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Invalid input. Please enter a valid integer for budget id: ");
            }

            Console.Write("Enter the budget limit: ");
            while (!double.TryParse(Console.ReadLine(), out limit))
            {
                Console.Write("Invalid input. Please enter a valid number for budget limit: ");
            }
            
            Console.Write("Enter the spending amount of this budget: ");
            while (!double.TryParse(Console.ReadLine(), out amountSpent))
            {
                Console.Write("Invalid input. Please enter a valid number for spending amount: ");
            }

            Console.Write("Enter the category: ");
            category = Console.ReadLine();

            loadeduser.budgetManager.editRecord(id, limit, amountSpent, category);
        }

        static void removeBudget(){
            int id;

            Console.Write("Enter the budget id: ");
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Invalid input. Please enter a valid integer for id: ");
            }

            loadeduser.budgetManager.deleteRecord(id);
        }

        static void remainderPage(){
            Console.WriteLine("=====================================");
            Console.WriteLine("               Remainders");
            Console.WriteLine("=====================================");

            
        }
    }
}