# Diary
ASP.NET MVC5 Diary and Contact list <br />
//Todo: Make good deploy instead of this <br />
Deploy: <br />
Repair connection string in Web.config:
1) Replace datasource with your instance name (LocalDB was being used at development)
2) Database should be "Diary"

Not realized:
1) Contact info validation (check that e-mail pass the regex etc.)
2) Adding contact information in time of creating contact (is it important?)

Развертывание: <br />
Исправить строку подключения в Web.config:
1) Заменить datasource именем экземпляра MSSqlServer (LocalDB использовался при разработке)
2) База данных должна быть "Diary"

Не реализовано:
1) Валидация контактной информации (проверка соответствия e-mail регулярному выражению)
2) Добавление контактной информации во время создания контакта (надо ли?)

Screenshots/Скриншоты:

Contact editing / редактирование контакта:
![alt text](https://github.com/CaptainIllidan/Diary/blob/master/Diary/Screenshots/ContactEdit.png)

Diary / ежедневник:
![alt text](https://github.com/CaptainIllidan/Diary/blob/master/Diary/Screenshots/Diary.png)
