# ASPLearning

## Description
Tasks of project:
1.	Create a web site with the following pages:
–	The Home page that contains links to other pages
–	The Categories page. Shows a list of categories without images
–	The Products page. Shows a table with the products
2.	 Add a configuration feature that supports two parameters:
–	Database connection string
–	Maximum (M) amount of products shown on the Product page (show only first M products, others – ignored; if M == 0, then show all products)
3.	 Add edit forms (New and Update) for the Products
4.	Add two client libraries to the project:
–	Bootstrap
–	jQuery Unobtrusive Validation
For Bootstrap: Apply Bootstrap styles to site pages/forms
5.	Change links to Categories and Product pages to navigation bar with the "hamburger" button
6.	For jQuery Unobtrusive Validation:
–	Configure client-side validation for edited products
7.	Configure logging:
–	configure writing logs into a log file
–	write the following events and information: application startup (Additional information: application location - folder path); configuration reading (Additional information: current configuration values)
8.	Create a custom error handler, which:
–	logs exception
–	returns error page with associated information (to look up appropriate records in the logs)
9.	Add separate projects with tests
10.	Add new Action into Category Controller, which return category image:
–	An image should be sent as a binary stream with the correct Content-Type (for referencing from HTML pages).Please note that test data for Northwind Categories contain broken images (it’s BMP pictures, but the first 78 bytes is garbage)
11.	Add links to images in the Category list.
12.	Add edit form for change image in Category (upload a new image)
Configure routing to enable getting images by not only the standard path {controller}/{action}/…, butalso by images/{image_id}
13.	Create your own middleware component for image caching. Middleware should:
–	Check Content-Type for every response, and, if any image of a valid format returned: Keep the image on the disk (as file);
If subsequent requests access the same image, return it from the cache directory;
–	Support the following options:
The path for the cache directory;
Max count of cached images;
Cache expiration time (if no requests during this time, cache cleaned)
–	Include the middleware into the request pipeline of your application
14.	Add MVC filter for logging calls to Actions. The filter should:
–	Log Action start/end
–	Provide an option to turn on/off logging parameters of the invoked Action method (off by default)
15.	Create a View Component to visualize simple breadcrumbs. View Component should show navigation in 2 variants:
–	Home > Entity > New / Edit (e.g. Home > Products > Create New) – if a user opens the edit/create form.
–	Home > Entity (e.g. Home > Products) – if a user opens the list/table of entities
–	Apply this component to all pages except the Home page.
16.	Create Html Helper and Tag Helper, which produce links to images in the format
as images/{image_id}. Usage of those helpers can look like:
–	Html Helper @Html.NorthwindImageLink(imageId, “Link text”);
–	Tag Helper <a northwind-id="imageId">Link text</a>
17.	Create API Controllers, which return collections of Categories and Products.
–	For Products, add the following operations: create, update, delete
–	For Categories, add the following operations: get and update image.
18.	Create two simple clients:
–	Console .Net [Core]
–	HTML Page + JS
19.	 Add generation of metadata and (optional) API documentation, based on Swashbuckle.AspNetCore.
20.	Create a separate unit test project, and generate client proxy with the use of any of the tools.
21.	Add user registration and authentication based on ASP.Net Core Identity. The following features should be implemented and available:
–	User self-registration
–	User authentication by login and password
–	Password reset (by sending an email with the secure URL)
22.	Integrate the website with Azure AD sign-in. User can choose which authentication provider to use: 
–	Login/password (local DB)
–	Azure AD
23.	Add authorization (access check) by the role model (where a role is a user group). For demonstration:
–	Add a new controller (for Administrators) with a single Action, which shows all site
 users
–	Allow access to this controller only for the Administrators group
24.	Add to the compilation pipeline the following steps:
–	Bundling and minification all .css and js files in the project
–	Precompilation of views




