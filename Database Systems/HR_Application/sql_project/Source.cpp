int findEmployee(MYSQL* conn, int employeeNumber, Employee* emp) {

	int exequery;
	int return_val = 0;

	// To store the returning result in  advanced before calling function mysql_store_result().
	MYSQL_RES* res;

	MYSQL_ROW Result_row;

	// 1) Exist the emp?
	string exist_query = "SELECT DISTINCT E.employeeNumber, E.lastName, E.firstName, E.email, F.phone, E.extension, concat(M.firstName, ' ', M.lastName) AS reportsTo, E.jobTitle, F.city FROM employees E left JOIN employees M on E.reportsTo = M.employeenumber INNER JOIN offices F on E.officeCode = F.officeCode WHERE E.employeeNumber = " + to_string(employeeNumber) + ";";
	const char* ex_q = exist_query.c_str(); // string to char

	// execute 'SQL query' using the mysql_query() function
	exequery = mysql_query(conn, ex_q);

	// query execution is successful
	if (!exequery) {
		res = mysql_store_result(conn);

		// To check if the result set is empty you examine the returning value
		if (mysql_num_rows(res) == 0) {
			// The Employee does not exist.
			return return_val; // return_val = 0
		}

		Result_row = mysql_fetch_row(res);

		emp->employeeNumber = atoi(Result_row[0]); // E.employeeNumber (string to int)
		strcpy(emp->lastName, Result_row[1]); // E.lastName
		strcpy(emp->firstName, Result_row[2]); // E.firstName
		strcpy(emp->email, Result_row[3]); // E.email
		strcpy(emp->phone, Result_row[4]); // F.phone
		strcpy(emp->extension, Result_row[5]); // E.extension
		if (Result_row[6] == NULL) {
			strcpy(emp->reportsTo, "");
		}
		else {
			strcpy(emp->reportsTo, Result_row[6]); // reportsTo
		}
		strcpy(emp->jobTitle, Result_row[7]); // E.jobTitle
		strcpy(emp->city, Result_row[8]); // F.city

		return return_val = 1;
	}

	//query execution is not successful
	else {
		cout << "Error message: " << mysql_error(conn) << ": " << mysql_errno(conn) << endl;
	} // if (!exequery)

}

// Displays a Emp
void displayEmployee(MYSQL* conn, Employee* emp) {

	cout << '\n' << "***************** Employee " << emp->employeeNumber << " *****************" << endl;

	cout << "employeeNumber = " << emp->employeeNumber << endl;
	cout << "lastName = " << emp->lastName << endl;
	cout << "firstName = " << emp->firstName << endl;
	cout << "email = " << emp->email << endl;
	cout << "phone = " << emp->phone << endl;
	cout << "extension = " << emp->extension << endl;
	cout << "reportsTo = " << emp->reportsTo << endl;
	cout << "jobTitle = " << emp->jobTitle << endl;
	cout << "city = " << emp->city << endl << endl;
}