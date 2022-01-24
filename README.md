# API DESARROLLADA: Properties US 

API que permite obtener información de Propiedades en Estados Unidos para una gran empresa de Real Estate.
```
	ÍNDICE:
	1. ARQUITECTURA
	2. ESTRUCTURA
	3. DOCUMENTACIÓN DE CÓDIGO
	4. RENDIMIENTO
	5. PRUEBAS UNITARIAS
	6. SEGURIDAD
	7. PRINCIPALES SERVICIOS EXPUESTOS
	8. CONFIGURACIÓN INICIAL DEL PROYECTO
```
## 1. ARQUITECTURA
Esta solución **API.PropertiesUS** tiene una arquitectura orientada a microservicios, la exposición de cada entidad de base de datos es independiente de las demás. También, pretende una organización de clases en _Capas_, actualmente la comunicación entre capas es mediante referencia de proyectos, sin embargo, a futuro puede implementarse la inyección de dependencias para individualizarlas completamente.

![Arquitectura del API](https://github.com/JohnHeiller/PropertiesUS/blob/main/DocumentedImages/1_Diagrama_arquitectura.png)

## 2. ESTRUCTURA
Como se mencionada, la organización de clases pretende una estructura en _Capas_, facilitando la ubicación de funcionalidades en caso de escalar la solución. Estas capas son: 

### 2.1. Controllers 
Contiene las clases con los servicios _REST_ por cada entidad 

![Estructura de Controladores](https://github.com/JohnHeiller/PropertiesUS/blob/main/DocumentedImages/2_Estructura_Controladores.png)

### 2.2. BL
Business Layer, contiene las clases con las validaciones de negocio o validaciones de estructura de datos, por cada entidad.
- **Contracts**
Contiene las interfaces de referencia para cada clase de validaciones para cada entidad
- **Secutiry**
Contiene la clase especial para validaciones de autenticación de usuario

![Estructura de BL](https://github.com/JohnHeiller/PropertiesUS/blob/main/DocumentedImages/2_Estructura_BL.png)

### 2.3. DAL
Data Access Layer, esta contiene las clases que administran (mediante _Entity Framework_) la base de datos, como el _DbContext_ y las clases _Repositorio_ por cada entidad.
- **Dominio**
Clases de referencia por cada entidad. Cada clase contiene la estructura de propiedades emparejada con una entidad de base de datos.
- **Repo**
Clase genérica base que crea un repositorio de métodos de acceso a cada entidad, es decir, un _CRUD_ para cada tabla. 

![Estructura de DAL](https://github.com/JohnHeiller/PropertiesUS/blob/main/DocumentedImages/2_Estructura_DAL.png)

### 2.4. DTO
Contiene las clases que actúan como objetos de transferencia de datos, encargados de conectar las peticiones del usuario al servicio con la gestión de la base de datos, estos se mapean con los _Dominios_ de cada entidad en la capa _BL_.

![Estructura de DTO](https://github.com/JohnHeiller/PropertiesUS/blob/main/DocumentedImages/2_Estructura_DTO.png)

Los archivos al nivel de la raíz del proyecto, o en el primer nivel del proyecto son los utilizados para la configuración de este:
- **API.PropertiesUS.xml**
Contiene la documentación de los servicios para exponerlos en _Swagger_.
- **appsettings.json**
Contiene los datos paramétricos relevantes para el funcionamiento de la aplicación.
- **Program.cs**
Esta clase define como iniciar el servidor web.
- **Startup.cs**
Esta clase permite configurar los servicios disponibles, como los servicios _REST_, la base de datos, seguridad, documentación.

## 3. DOCUMENTACIÓN DE CÓDIGO
El código está documentado en la cabecera de cada método y clase, en idioma inglés para facilitar el entendimiento y futura escalabilidad.
Los servicios expuestos permiten su prueba mediante _Swagger_, con este mismo se entrega una descripción de cada servicio para cada entidad.

![Documentación con Swagger](https://github.com/JohnHeiller/PropertiesUS/blob/main/DocumentedImages/3_Documentacion_Swagger.png)

## 4. RENDIMIENTO
- Se pretendió no recuperar más datos de los necesarios. Se escribieron consultas simples por cada entidad.
- Se pretendió minimizar los recorridos de ida y vuelta de red, recuperando los datos necesarios en una sola llamada en lugar de en varias llamadas.
- Se filtró y agregó consultas _LINQ_ para que la base de datos realice filtrado.
- Se utilizó la agrupación de _DbContext_, con _AddDbContextPool_. Esto siguiendo las recomendaciones de _EF_ _Core_ para una aplicación de _ASP.NET_ _Core_.

## 5. PRUEBAS UNITARIAS
Se crea el proyecto de pruebas unitarias **API.PropertiesUS.Test** con _NUnit_, probando los métodos de las clases en _BL_ mediante casos de pruebas preestablecidos.

![Proyecto de pruebas unitarias](https://github.com/JohnHeiller/PropertiesUS/blob/main/DocumentedImages/5_Pruebas_Unitarias.png)

## 6. SEGURIDAD
Se implementa una validación de Autenticación básica, esta limita el uso de los servicios sin que se registre el usuario y contraseña parametrizados. Los credenciales de acceso o autenticación se parametrizan en el archivo _appsettings.json_.

![Seguridad con Autenticación](https://github.com/JohnHeiller/PropertiesUS/blob/main/DocumentedImages/6_Seguridad_Autenticacion.png)

## 7. PRINCIPALES SERVICIOS EXPUESTOS

### 7.1.	Registrar o crear una propiedad: 
Para este caso, utilice el servicio POST _/Property/Create_, enviando un _JSON_ en el body con la estructura del _PropertyDTO_, ej.:
```sh
	{
	  "nameProperty": "Edificio Caobos",
	  "addressProperty": "Calle 10 # 11 - 1",
	  "priceProperty": "1000000",
	  "codeInternalProperty": "",
	  "year": "2005",
	  "idOwner": null,
	  "nameOwner": "Usuario1"
	}
```
En los dos últimos campos, _idOwner_ y _nameOwner_, se espera un valor que indique el registro de _Owner_ a asociar con la nueva _Property_.

### 7.2.	Agregar imagen de una propiedad
En este caso, se utiliza el servicio POST _/PropertyImage/Add_, enviando un _JSON_ en el body con la estructura del _PropertyImageDTO_, ej.: 
```sh
	{
	  "fileImage": null,
	  "imageBase64": "/9j/4AAQSkZJRgABAQEAZABkAAD/7QNqUGhvdG9zaG9wIDMuMAA4QklNBAQAAA...",
	  "codeInternalProperty": "",
	  "nameProperty": "",
	  "idProperty": 1
	}
```
En esta estructura puede enviar una imagen de dos maneras: la primera y más recomendable es mediante el campo _imageBase64_, el cual espera un texto con la imagen decodificada en Base64, facilitando su registro, y la segunda manera es mediante el campo _fileImage_, en el cual puede enviar la imagen de tipo _System.Drawing.Image_.
En el campo _idProperty_ debe enviar el identificador del registro de _Property_ a relacionar con la imagen.

### 7.3.	Cambiar precio de una propiedad
En este caso, se utiliza el servicio POST _/Property/ChangePrice_, enviando un _JSON_ en el body con la estructura del _PropertyPriceDTO_, ej.:
```sh
	{
	  "price": "550000",
	  "name": "",
	  "codeInternal": "",
	  "idProperty": 1
	}
```
En este, debe enviar el nuevo valor del precio de la propiedad en el campo _Price_, y para relacionar el registro de _Property_ puede utilizar cualquiera de los últimos 3 campos _name_, _codeInternal_, _idProperty_, enviando el respectivo valor.

### 7.4.	Actualizar propiedad
Para este caso, se utiliza el servicio POST _/Property/Update_, enviando un _JSON_ en el body con la estructura del _PropertyDTO_, ej.:
```sh
	{
	  "idProperty": 5,
	  "nameProperty": "Edificio Caobos",
	  "addressProperty": "Calle 10 # 11 - 1",
	  "priceProperty": "1000000",
	  "codeInternalProperty": "",
	  "year": "2005",
	  "idOwner": 1
	}
```
Donde debe enviar, obligatoriamente, el campo _idProperty_ con el identificador del registro de _Property_ a actualizar. El resto de los campos se permiten actualizar.

### 7.5.	Listar propiedades con filtros de búsqueda
En este caso, se utiliza el servicio POST _/Property/GetListByFilters_, enviando un _JSON_ en el body con la estructura del _PropertyDTO_, ej.:
```sh
	{
	  "nameProperty": "",
	  "addressProperty": "",
	  "priceProperty": "550000",
	  "codeInternalProperty": "",
	  "year": "",
	  "idOwner": 3,
	  "nameOwner": ""
	}
```
En este, se podrá enviar el valor especifico que se quiere utilizar como filtro para obtener una lista de registros desde _Property_.

## 8. CONFIGURACIÓN INICIAL DEL PROYECTO

### 8.1.	Descargue o clone el proyecto desde GitHub a un repositorio local

### 8.2.	Configure la base de datos del API:
- Cree la base de datos _PropertiesUS_. Para esto puede utilizar el script _CREATE_DATABASE_PropertiesUS.sql_ ubicado en la carpeta _BD_Scripts_ dentro del repositorio.
- Cree las tablas respectivas para conectar el proyecto. Para esto puede ejecutar los scripts ubicados en la carpeta _BD_Scripts_ dentro del repositorio _CREATE_dbo.Owner.sql_, _CREATE_dbo.Property.sql_, _CREATE_dbo.PropertyImage.sql_, _CREATE_dbo.PropertyTrace.sql_, y sobre esto puede obtener referencia sobre las conexiones entre tablas con la imagen _DiagramaBD.JPG_, dentro de la misma carpeta. También puede migrar la base de datos desde el mismo proyecto, para lo cual debe asignar la cadena de conexión de la base de datos como valor para la variable _dbParameter_ dentro de la clase _DbContextPropertiesUS_, tome como ejemplo la imagen siguiente:  

![Migración de BD](https://github.com/JohnHeiller/PropertiesUS/blob/main/DocumentedImages/8_1_Migrar_BD.png)

### 8.3.	Configure y lance el API:
- Una vez configurada la base de datos puede dejar la parametrización de la cadena de conexión para que se obtenga desde una variable de entorno. 

![Configuración conexión a API](https://github.com/JohnHeiller/PropertiesUS/blob/main/DocumentedImages/8_2_Usar_Cadena_BD.png)

Recuerde que estas variables de entorno están en el archivo de configuración _appsettings.json_, nombrada especificamente como _APIConnection_, aquí deberá colocar el valor de la cadena de conexión de su base de datos. 

![Parámetro APIConnection](https://github.com/JohnHeiller/PropertiesUS/blob/main/DocumentedImages/8_3_APIConnection.png)

- Finalmente, puede ejecutar el proyecto para visualizar la exposición de los servicios creados. 

![Autenticación en Swagger](https://github.com/JohnHeiller/PropertiesUS/blob/main/DocumentedImages/8_4_Authorize_Swagger.png)

Recuerde que, para hacer una petición a uno de estos servicios, primero debe autenticarse. 

![Uso de credenciales de acceso en Swagger](https://github.com/JohnHeiller/PropertiesUS/blob/main/DocumentedImages/8_5_Credenciales_Autenticacion.png)

Y las credenciales de acceso están parametrizadas en el archivo _appsettings.json_. 

![Parametrización de credenciales de acceso de API](https://github.com/JohnHeiller/PropertiesUS/blob/main/DocumentedImages/8_6_Parametrizacion_Credenciales.png)

Posteriormente, con la autenticación con credenciales correctas, se podrán probar normalmente los servicios desde la interfaz _Swagger_. 

![Prueba de servicios en Swagger](https://github.com/JohnHeiller/PropertiesUS/blob/main/DocumentedImages/8_7_Servicios_Swagger.png)
