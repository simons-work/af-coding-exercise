@echo This will create a database called 'Insurance' in SQL instance '(localdb)\\MSSQLLocalDB'
@pause

cd Web.Api
dotnet ef database update

@echo Database creation completed (hopefully!)
@pause
