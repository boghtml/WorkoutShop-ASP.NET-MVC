<h1 align="center">WorkoutShop</h1>

<p align="center">
  <strong>WorkoutShop</strong> is an online store for sports equipment and accessories, developed using ASP.NET Core MVC. The project aims to provide users with a convenient and intuitive interface for browsing and purchasing sports products.
</p>

<p align="center">
  <img src="https://th.bing.com/th/id/OIP.5DpZoMomv2rQPqO6e61CwQHaHa?w=200&h=200&c=7&r=0&o=5&dpr=1.3&pid=1.7" alt="WorkoutShop Logo" class="d-inline-block align-top">
</p>

<h2>Features</h2>

<ul>
  <li><strong>Product Catalog:</strong> View products with the ability to filter and sort by various parameters.</li>
  <li><strong>Detailed View:</strong> Detailed product information, image gallery, and the ability to add items to the cart.</li>
  <li><strong>Shopping Cart:</strong> Manage items in the cart, change quantities, remove items, and view the total amount.</li>
  <li><strong>Checkout:</strong> Order placement process with input of contact information and confirmation.</li>
  <li><strong>User Account:</strong> View order history, filter, and sort orders.</li>
  <li><strong>Admin Panel:</strong> Manage products, categories, and orders for administrators.</li>
  <li><strong>Authentication System:</strong> Registration, login, and differentiation of access rights between users and administrators.</li>
</ul>

<h2>Technologies and Tools</h2>

<ul>
  <li><a href="https://docs.microsoft.com/aspnet/core/?view=aspnetcore-6.0" target="_blank">ASP.NET Core MVC</a></li>
  <li><a href="https://docs.microsoft.com/ef/core/" target="_blank">Entity Framework Core</a></li>
  <li><a href="https://www.microsoft.com/sql-server" target="_blank">Microsoft SQL Server</a></li>
  <li><a href="https://getbootstrap.com/" target="_blank">Bootstrap 5</a></li>
  <li><a href="https://jquery.com/" target="_blank">jQuery</a></li>
  <li><a href="https://fontawesome.com/" target="_blank">Font Awesome</a></li>
  <li><a href="https://docs.microsoft.com/aspnet/core/security/authentication/identity" target="_blank">ASP.NET Identity</a></li>
</ul>

<h2>Installation and Launch</h2>

<ol>
  <li>Clone the repository:
    <pre><code>git clone https://github.com/your_username/WorkoutShop.git</code></pre>
  </li>
  <li>Navigate to the project directory:
    <pre><code>cd WorkoutShop</code></pre>
  </li>
  <li>Open the project in Visual Studio or Visual Studio Code.</li>
  <li>Configure the database connection string in the <code>appsettings.json</code> file:
    <pre><code>
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=WorkoutShopDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
    </code></pre>
  </li>
  <li>Run migrations to create the database:
    <pre><code>Update-Database</code></pre>
    *Or from the command line:*
    <pre><code>dotnet ef database update</code></pre>
  </li>
  <li>Start the application:
    <pre><code>dotnet run</code></pre>
  </li>
  <li>Open your browser and navigate to <a href="https://localhost:5001" target="_blank">https://localhost:5001</a>.</li>
</ol>

<h2>Usage Examples</h2>
<!-- You can add usage examples here -->



<p>Home Page:</p>

<p align="center">

![image](https://github.com/user-attachments/assets/c352445b-5ab2-4fee-a50d-49fc8de68132)
![image](https://github.com/user-attachments/assets/7776e12f-c304-4cda-a79e-7eb8cb4a7667)
</p>

<p>Product Page:</p>

<p align="center">

![image](https://github.com/user-attachments/assets/bf4856e6-da0b-459c-8155-d6662c5f6641)
![image](https://github.com/user-attachments/assets/fb80c6a7-95ae-42ba-ad06-974a195d1a48)
</p>

<p>Map and Ordering:</p>

<p align="center">

![image](https://github.com/user-attachments/assets/a9f53e12-04da-4d64-8039-8a6f1002b89f)
![image](https://github.com/user-attachments/assets/fdaafadf-ca19-4460-9aed-db12a0b9c47f)
![image](https://github.com/user-att actions/assets/4552dbdc-b3b1-48d2-919c-34d22d8a4b8e)
![image](https://github.com/user-attachments/assets/3da182fb-7914-4e34-840a-dbe9f2f00472)
![image](https://github.com/user-attachments/assets/16ed9494-2773-4110-abc4-e214c7279ab9)
</p>


