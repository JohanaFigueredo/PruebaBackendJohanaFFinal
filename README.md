# PruebaBackendJohanaFFinal
PRUEBA TÉCNICA .NET CORE

Por favor realizar un API con un CRUD básico de Usuarios (Crear, Leer, Editar y Eliminar) en .NET Core 3+ (MVC o API) con base de datos PostgreSQL (preferiblemente) o SQL.

La tabla Usuarios debe guardar los siguientes datos:
-	Id
-	Nombre completo
-	Cédula
-	Fecha de nacimiento
-	Correo electrónico
-	Teléfono
-	Organización para la que trabaja 

Adicional se tendrá una tabla de Organizaciones (se debe crear en la base de datos, más no crear un CRUD para ella), con los siguientes datos:
-	Id
-	Nombre
-	Dirección

Se valorará el uso de:
-	Estructura / Arquitectura implementada
-	Dependency injection
-	Logs para errores
-	Mensajes de error y éxito
-	Validaciones de tipo de datos (los nombres no deben llevar números, la cédula no debe llevar letras, etc)
-	Validaciones de datos únicos (cédula, correo electrónico y teléfono no se repiten) 
-	Código limpio y ordenado
