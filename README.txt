Intro
=====================================================================

This repository is a selection of contributions I made to a project
for a client, Blue Ribbons Reviews.

Each folder contains screenshots of the diffs in each file and complete code of the changed files. The folders contain, where appropriate, screenshots of the added features on the site.

Notes on the Changesets are provided below. See especially Changesets marked with a *.

Highlights From the Project
=====================================================================

Changeset 1438: User Story 1116: Added sort functionality to Index page for Campaigns Controller
	Sort by
		-Title
		-Sale Price
		-StartDate
		-Close Date

Changeset 1439: Modified Sort Feature on Campaign Index page to have Dropdown and Changed Options

*Changeset 1475: User Story 1190: modified the Html.BeginForm to accept route values to pass to the Register method of the Account Controller the value of the radio button along with the properties of the model. Used bool isBuyer in the Register method of AccountController to correspond to the radio buttons' 'name' and 'value'. (See Changeset 1562.)

*Changeset 1499
User Story 1200: Added a hide/show function to the "Sign In" "Register" Buttons on the register page

Changeset 1520: User Story 1228: added navigation properties for the Review and Campaign icollections to the IdentityModel

Changeset 1529: User Story 1248: changed the "Login Page button the Details Modal on the Campaigns Index page to say "Sign In" and have the same classes as the "See details" button on the campaign index.
Plus, fixed the Sign In button to redirect to the Register/ Sign In page correctly and fixed the redirect after registering to correctly use Url.Action() instead of hard coding the "Login Page" button.

Changeset 1547: User Story 1252: When a register action fails, let the register page know so that the register container could be re-displayed and and error could be displayed asking the user to try again. Note this took a few changesets, so the recorded changes are just the last changes.

*Changeset 1548: User Story 1253: Modifed the adminviewmodel to load the identity model itself instead of the identity properties

Changeset 1554: User Story 1273: added a dropdown list element in the navbar called "Seller Dashboard" with two dropdown options "Reviews" (which links to the Index views for Reviews controller) and "Campaigns" (which links to the Index views for the Campaigns controller)

*Changeset 1562: User Story 1274: updated the Register method in the account controller to add records to the AspNetUserRoles table to take advantage of the isBuyer boolean, using that to determine which role to assign to user. (See Changeset 1475.)

Changeset 1563: User Story 1281: scaffolded a new view for the Campaign controller called AdminView of the List template

Changeset 1569: User Story 1282: tailored AdminView to suit the needs of the Admin
	Displaying the following:
	Seller's First Name
	Seller's Last Name
	Seller's Email
	Campaign Name
	Campaign OpenCampaign
	Campaign RetailPrice
	Campaign SalePrice
	Campaign StartDate
	Campaign CloseDate
	Campaign ExpireDate
	
Changeset 1579: User Story 1283: Added sort functionality to the AdminView pa1ge for Campaigns Controller

