OMES Asset Tracking Application
======

## Project Overview
The project is a mobile application that allows OMES employees to scan/read the barcode on OMES assets and send an email to a server with information related to that asset, such as employee currently using it, the asset itself, and GPS coordinates for keeping track of that asset. The application is intended to streamline the tracking of assets so as to make sure the assets are not lost.

OMES employees are required to scan assets they currently have on a yearly basis, but each individual employee may check in at a different month of the year, depending on hire date. 

Employees will be emailed a reminder on their check-in month containing a code to "login" to the application - this code will change monthly to prevent misuse of the application.

The employee then enters their 6-digit employee ID number - contractors append a C to their ID number. This is to indicate who currently owns the asset. It is possible for an employee to enter their coworker's ID as a courtesy to check-in the asset.

Then the employee will scan the barcode attached to the asset. After a successful scan, the employee may wish to continue scanning additional assets. After the employee has scanned all assets, they submit the assets to be sent in an email by the application - each asset will send an email containing information about that asset.

## Functional Requirements
### Must Have
* iOS compatibility
* GPS tracking
* Barcode scanning
* Code validation that changes monthly
### Desirable
* Android compatibility
* Picture capture - reading a bar code from picture

## System Constraints
* Programming Language: C#

## Other Notes
The application should be convenient to use. Security is not a large issue.

## To Be Determined
* Look/feel and design of the app.
 * This will be discussed at a second meeting when we begin developing the app.
* Email format