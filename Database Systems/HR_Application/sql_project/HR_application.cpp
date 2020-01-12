/***********************************************************************
// HR application using the C++ programming language and MySQL server
//
	▶ Name: Youjin An
	▶ Email : auj0827@gmail.com

	** Please understand that the MySQL server in DBS201 at Seneca is closed and cannot be executed.

***********************************************************************/

#define _CRT_SECURE_NO_WARNINGS

#include <mysql.h>

#include <iostream>
#include <iomanip>
#include <cstring>

#include <sstream>

using namespace std;

struct Employee {
	int employeeNumber;
	char lastName[50];
	char firstName[50];
	char email[100];
	char phone[50];
	char extension[10];
	char reportsTo[100]; // stores the first name and the last name of the employee who is the manager of the given employee number.
	char jobTitle[50];
	char city[50];
} emp;

// Performs a fool-proof integer entry
int getInt(int min, int max);
// displays the user interface menu
int menu(void);

int findEmployee(MYSQL* conn, int employeeNumber, Employee* emp);
void displayEmployee(MYSQL* conn, Employee* emp);		// Displays all members of the emp parameter.
void displayAllEmployees(MYSQL* conn);					// Displays all employees’ information if exists
void insertEmployee(MYSQL* conn, Employee* emp);		
void updateEmployee(MYSQL* conn, int employeeNumber);
void deleteEmployee(MYSQL* conn, int employeeNumber);

int main(void) {

	MYSQL* conn;
	conn = mysql_init(0);

	// connect with MYSQL Database
	conn = mysql_real_connect(conn, "mymysql.senecacollege.ca", "db_yan24", "&d97l>MdDp", "db_yan24", 3306, nullptr, 0);

	// the connection is successfully established
	if (conn) {
		cout << "successful connection to database" << endl << endl;

		int selected_option = 0;
		int employeeNumber = 0, check = 0;

		/* Menu */
		do {
			selected_option = menu();

			switch (selected_option) {
			case 0: // Exit the program
				cout << "Exiting Employee Data Program. Good Bye!!!" << endl;
				break;

			case 1: // Find Employee
				// Prompt the user to enter a value for the employee number
				cout << "Find Employee number: ";
				employeeNumber = getInt(0, 2000);

				// Check if the employee with the given employee number exists
				check = findEmployee(conn, employeeNumber, &emp);

				if (check == 0) {
					// If the returning value of function findEmployee() is 0, display a proper error message.
					cout << "Employee " << employeeNumber << " does not exist." << endl << endl;
				}
				else {
					// To display the employee information
					displayEmployee(conn, &emp);
				}

				break;

			case 2: // Employees Report
				displayAllEmployees(conn);
				break;

			case 3: // Add Employee
				cout << "********************* ADD emp *********************" << endl;
				// Prompt the user to enter a value for the employee number
				cout << "Employee Number: ";
				employeeNumber = getInt(0, 2000);
				emp.employeeNumber = employeeNumber;
				cout << "Last Name: ";
				cin >> emp.lastName;
				cout << "Fist Name: ";
				cin >> emp.firstName;
				cout << "Email: ";
				cin >> emp.email;
				cout << "extension: ";
				cin >> emp.extension;
				cout << "Job Title: ";
				cin >> emp.jobTitle;
				cout << "City: ";
				cin >> emp.city;

				// Check if the employee with the given employee number exists
				check = findEmployee(conn, employeeNumber, &emp);

				if (check == 1) {
					// If the returning value of function findEmployee() is 1, display a proper error message.
					cout << "An employee with the same employee number exists." << endl << endl;
				}
				else {
					// Add the employee
					insertEmployee(conn, &emp);
				}
				
				break;

			case 4: // Update Employee
				cout << "********************* Upd emp *********************" << endl;
				// Prompt the user to enter a value for the employee number
				cout << "Employee Number: ";
				employeeNumber = getInt(0, 2000);

				// Check if the employee with the given employee number exists
				check = findEmployee(conn, employeeNumber, &emp);

				if (check == 0) {
					// If the returning value of function findEmployee() is 0, display a proper error message.
					cout << "Employee " << employeeNumber << " does not exist." << endl << endl;
				}
				else {
					// If employee does exist, ask the user to enter the new phone extension. 
					updateEmployee(conn, employeeNumber);
				}

				break;

			case 5: //Remove Employee
				cout << "********************* Del emp *********************" << endl;
				// Prompt the user to enter a value for the employee number
				cout << "Employee Number: ";
				employeeNumber = getInt(0, 2000);

				// Check if the employee with the given employee number exists
				check = findEmployee(conn, employeeNumber, &emp);

				if (check == 0) {
					// If the returning value of function findEmployee() is 0, display a proper error message.
					cout << "Employee " << employeeNumber << " does not exist." << endl << endl;
				}
				else {
					// If employee does exist
					deleteEmployee(conn, employeeNumber);
				}

				break;
			} // switch
		} while (selected_option != 0);
	} // if (coon)

	// the connection is not successfully established
	else {
		cout << "Connection Failed" << mysql_error(conn) << endl;
	}

	// the connection to the database is no longer required
	mysql_close(conn);

	return 0;
}

// Performs a fool-proof integer entry
int getInt(int min, int max) {
	int selected_option;
	bool done = false;

	while (!done) {
		cin >> selected_option;
		if (cin.fail()) {
			cin.clear();
			cout << "\n" << "ERROR: Incorrect Option!" << "\n" << "Try Again: ";
		}
		else {
			if (selected_option < min || selected_option > max) {
				cout << "\n" << "ERROR: Invalid value!" << endl << "The value must be between " << min << " and " << max << endl;
				cout << "Try Again: ";
			}
			else {
				done = true;
			}
		}
		cin.ignore(1000, '\n');
	}
	return selected_option;
}

int menu(void) {

	int selected_option = 0;

	cout << "********************* HR Menu *********************" << endl;

	// Print the option list
	cout << "1) Find Employee" << endl;
	cout << "2) Employees Report" << endl;
	cout << "3) Add Employee" << endl;
	cout << "4) Update Employee" << endl;
	cout << "5) Remove Employee" << endl;
	cout << "0) Exit" << endl << endl;

	// Prompt the user to enter an option
	cout << "Select the number: ";
	selected_option = getInt(0, 5);
	cout << endl;

	return selected_option;
}

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

// Displays all employees’ information if exists
void displayAllEmployees(MYSQL* conn) {
	int exequery;

	MYSQL_RES* Total_res;
	MYSQL_ROW Total_row;

	string emp_query = "SELECT E.employeeNumber, concat(E.firstName, ' ', E.lastName), E.email, F.phone, E.extension, concat(M.firstName, ' ', M.lastName) AS reportsTo FROM employees E left JOIN employees M on E.reportsTo = M.employeenumber INNER JOIN offices F on E.officeCode = F.officeCode;";
	const char* emp_q = emp_query.c_str(); // string to char

	exequery = mysql_query(conn, emp_q);

	//query execution is successful
	if (!exequery) {
		Total_res = mysql_store_result(conn);

		if (mysql_num_rows(Total_res) == 0) {
			cout << "There is no employees’ information to be displayed." << endl;
		}

		cout << endl;
		cout.setf(ios::left);
		cout << setw(7) << "E" << setw(25) << "Employee Name" << setw(35) << "Email" << setw(20) << "Phone" << setw(10) << "Ext" << setw(7) << "Manager" << endl;
		cout << "------------------------------------------------------------------------------------------------------------------------" << endl;
		while (Total_row = mysql_fetch_row(Total_res)) {
			cout << setw(7) << Total_row[0] << setw(25) << Total_row[1] << setw(35) << Total_row[2] << setw(20) << Total_row[3] << setw(10) << Total_row[4];
			if (Total_row[5] == NULL) {
				cout << endl;
				continue;	
			}
			cout << setw(7) << Total_row[5] << endl;
		}
		cout << endl;
	}

	else {
		//query execution is not successful
		cout << "Error message: " << mysql_error(conn) << ": " << mysql_errno(conn) << endl;
	} // if (!exequery)

}

void insertEmployee(MYSQL* conn, Employee* emp) {

	int exequery;

	// To store the returning result in  advanced before calling function mysql_store_result().
	MYSQL_RES* res;

	// 1) Exist the emp?
	// the office code of the new employees is 1 and the manager id (reportsTo)is 102 by default.
	const string quote = "\"";
	string exist_query = "INSERT INTO employees (employeeNumber, lastName, firstName, email, officeCode, reportsTo, extension, jobTitle) VALUES (" + to_string(emp->employeeNumber) + "," + quote + emp->lastName + quote + "," + quote + emp->firstName + quote + "," + quote + emp->email + quote + "," + quote + "1" + quote + "," + quote + "1002" + quote + "," + quote + emp->extension + quote + "," + quote + emp->jobTitle + quote + ");";
	
	const char* ex_q = exist_query.c_str(); // string to char

	// execute 'SQL query' using the mysql_query() function
	exequery = mysql_query(conn, ex_q);

	// query execution is successful
	if (!exequery) {
		cout << "The new employee is added successfully." << endl << endl;
	}

	//query execution is not successful
	else {
		cout << "Error message: " << mysql_error(conn) << ": " << mysql_errno(conn) << endl;
	} // if (!exequery)

}

void updateEmployee(MYSQL* conn, int employeeNumber) {

	string temp_extension = "x0000";

	cout << "New Extension: ";
	cin >> temp_extension;

	int exequery;

	// To store the returning result in  advanced before calling function mysql_store_result().
	MYSQL_RES* res;

	// 1) Exist the emp?
	// the office code of the new employees is 1 and the manager id (reportsTo)is 102 by default.
	const string quote = "\"";
	string exist_query = "UPDATE employees SET extension = " + quote + temp_extension + quote + "WHERE employeeNumber = " + to_string(employeeNumber) + ";";

	const char* ex_q = exist_query.c_str(); // string to char

	// execute 'SQL query' using the mysql_query() function
	exequery = mysql_query(conn, ex_q);

	// query execution is successful
	if (!exequery) {
		cout << "Employee " << employeeNumber << "'s extension is changed " << temp_extension << " successfully." << endl << endl;
	}

	//query execution is not successful
	else {
		cout << "Error message: " << mysql_error(conn) << ": " << mysql_errno(conn) << endl;
	} // if (!exequery)
}

void deleteEmployee(MYSQL* conn, int employeeNumber) {
	int exequery;

	// To store the returning result in  advanced before calling function mysql_store_result().
	MYSQL_RES* res;

	// 1) Exist the emp?
	// the office code of the new employees is 1 and the manager id (reportsTo)is 102 by default.
	const string quote = "\"";
	string exist_query = "DELETE FROM employees WHERE employeeNumber = " + to_string(employeeNumber) + "; ";

	const char* ex_q = exist_query.c_str(); // string to char

	// execute 'SQL query' using the mysql_query() function
	exequery = mysql_query(conn, ex_q);

	// query execution is successful
	if (!exequery) {
		cout << "Employee " << employeeNumber << " is deleted successfully." << endl << endl;
	}

	//query execution is not successful
	else {
		cout << "Error message: " << mysql_error(conn) << ": " << mysql_errno(conn) << endl;
	} // if (!exequery)
}