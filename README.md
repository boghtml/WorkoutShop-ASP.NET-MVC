<h1 align="center">WorkoutShop</h1>

<p align="center">
  <strong>WorkoutShop</strong> - це інтернет-магазин спортивного обладнання та аксесуарів, розроблений на основі ASP.NET Core MVC. Проект має на меті надати користувачам зручний та інтуїтивно зрозумілий інтерфейс для перегляду та придбання спортивних товарів.
</p>

<p align="center">
  <img src="https://th.bing.com/th/id/OIP.5DpZoMomv2rQPqO6e61CwQHaHa?w=200&h=200&c=7&r=0&o=5&dpr=1.3&pid=1.7" alt="WorkoutShop Logo" class="d-inline-block align-top">
</p>

<h2>Функціональні можливості</h2>

<ul>
  <li><strong>Каталог товарів:</strong> Перегляд товарів з можливістю фільтрації та сортування за різними параметрами.</li>
  <li><strong>Детальний перегляд:</strong> Детальна інформація про товар, галерея зображень та можливість додавання до кошика.</li>
  <li><strong>Кошик покупця:</strong> Управління товарами в кошику, зміна кількості, видалення та перегляд загальної суми.</li>
  <li><strong>Оформлення замовлення:</strong> Процес оформлення замовлення з введенням контактних даних та підтвердженням.</li>
  <li><strong>Особистий кабінет:</strong> Перегляд історії замовлень, фільтрація та сортування замовлень.</li>
  <li><strong>Адміністративна панель:</strong> Управління товарами, категоріями та замовленнями для адміністраторів.</li>
  <li><strong>Система аутентифікації:</strong> Реєстрація, вхід та розмежування прав доступу між користувачами та адміністраторами.</li>
</ul>

<h2>Технології та інструменти</h2>

<ul>
  <li><a href="https://docs.microsoft.com/aspnet/core/?view=aspnetcore-6.0" target="_blank">ASP.NET Core MVC</a></li>
  <li><a href="https://docs.microsoft.com/ef/core/" target="_blank">Entity Framework Core</a></li>
  <li><a href="https://www.microsoft.com/sql-server" target="_blank">Microsoft SQL Server</a></li>
  <li><a href="https://getbootstrap.com/" target="_blank">Bootstrap 5</a></li>
  <li><a href="https://jquery.com/" target="_blank">jQuery</a></li>
  <li><a href="https://fontawesome.com/" target="_blank">Font Awesome</a></li>
  <li><a href="https://docs.microsoft.com/aspnet/core/security/authentication/identity" target="_blank">ASP.NET Identity</a></li>
</ul>

<h2>Встановлення та запуск</h2>

<ol>
  <li>Клонуйте репозиторій:
    <pre><code>git clone https://github.com/your_username/WorkoutShop.git</code></pre>
  </li>
  <li>Перейдіть в каталог проекту:
    <pre><code>cd WorkoutShop</code></pre>
  </li>
  <li>Відкрийте проект в Visual Studio або Visual Studio Code.</li>
  <li>Налаштуйте рядок підключення до бази даних в файлі <code>appsettings.json</code>:
    <pre><code>
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=WorkoutShopDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
    </code></pre>
  </li>
  <li>Виконайте міграції для створення бази даних:
    <pre><code>Update-Database</code></pre>
    *Або з командного рядка:*
    <pre><code>dotnet ef database update</code></pre>
  </li>
  <li>Запустіть застосунок:
    <pre><code>dotnet run</code></pre>
  </li>
  <li>Відкрийте браузер та перейдіть за адресою <a href="https://localhost:5001" target="_blank">https://localhost:5001</a>.</li>
</ol>


<h2>Приклади використання</h2>

<p>Головна сторінка:</p>

![image](https://github.com/user-attachments/assets/c352445b-5ab2-4fee-a50d-49fc8de68132)
![image](https://github.com/user-attachments/assets/7776e12f-c304-4cda-a79e-7eb8cb4a7667)

<p>Сторінка товару:</p>

![image](https://github.com/user-attachments/assets/bf4856e6-da0b-459c-8155-d6662c5f6641)
![image](https://github.com/user-attachments/assets/fb80c6a7-95ae-42ba-ad06-974a195d1a48)

<p>Карта та офрмлення замовлення:</p>

![image](https://github.com/user-attachments/assets/a9f53e12-04da-4d64-8039-8a6f1002b89f)
![image](https://github.com/user-attachments/assets/fdaafadf-ca19-4460-9aed-db12a0b9c47f)
![image](https://github.com/user-attachments/assets/4552dbdc-b3b1-48d2-919c-34d22d8a4b8e)
![image](https://github.com/user-attachments/assets/3da182fb-7914-4e34-840a-dbe9f2f00472)
![image](https://github.com/user-attachments/assets/16ed9494-2773-4110-abc4-e214c7279ab9)








