# API DESARROLLADA: Properties US 

API que permite obtener información de Propiedades en Estados Unidos para una gran empresa de Real Estate.

## 1. ARQUITECTURA
Esta solución tiene una arquitectura orientada a microservicios, la exposición de cada entidad de base de datos es independiente de las demás. También, pretende una organización de clases en Capas, actualmente la comunicación entre capas es mediante referencia de proyectos, sin embargo, a futuro puede implementarse la inyección de dependencias para individualizarlas completamente.

## 2. ESTRUCTURA
Como se mencionada, la organización de clases pretende una estructura en Capas, facilitando la ubicación de funcionalidades en caso de escalar la solución. Estas capas son: 
### 2.1. Controllers 
Contiene las clases con los servicios REST por cada entidad
### 2.2. BL
Business Layer, contiene las clases con las validaciones de negocio o validaciones de estructura de datos, por cada entidad.
- **Contracts**
Contiene las interfaces de referencia para cada clase de validaciones para cada entidad
- **Secutiry**
Contiene la clase especial para validaciones de autenticación de usuario
### 2.3. DAL
Data Access Layer, esta contiene las clases que administran (mediante Entity Framework) la base de datos, como el DbContext y las clases Repositorio por cada entidad.
- **Dominio**
Clases de referencia por cada entidad. Cada clase contiene la estructura de propiedades emparejada con una entidad de base de datos.
- **Repo**
Clase genérica base que crea un repositorio de métodos de acceso a cada entidad, es decir, un CRUD para cada tabla.
### 2.4. DTO
Contiene las clases que actúan como objetos de transferencia de datos, encargados de conectar las peticiones del usuario al servicio con la gestión de la base de datos, estos se mapean con los Dominios de cada entidad en la capa BL.

Los archivos al nivel de la raíz del proyecto, o en el primer nivel del proyecto son los utilizados para la configuración de este:
- **API.PropertiesUS.xml**
Contiene la documentación de los servicios para exponerlos en Swagger.
- **appsettings.json**
Contiene los datos paramétricos relevantes para el funcionamiento de la aplicación.
- **Program.cs**
Esta clase define como iniciar el servidor web.
- **Startup.cs**
Esta clase permite configurar los servicios disponibles, como los servicios REST, la base de datos, seguridad, documentación.

## 3. DOCUMENTACIÓN DE CÓDIGO
El código está documentado en la cabecera de cada método y clase, en idioma inglés para facilitar el entendimiento y futura escalabilidad.
Los servicios expuestos permiten su prueba mediante Swagger, con este mismo se entrega una descripción de cada servicio para cada entidad.

## 4. RENDIMIENTO
Se pretendió no recuperar más datos de los necesarios. Se escribieron consultas simples por cada entidad.
Se pretendió minimizar los recorridos de ida y vuelta de red, recuperando los datos necesarios en una sola llamada en lugar de en varias llamadas.
Se filtró y agregó consultas LINQ para que la base de datos realice filtrado.
Se utilizó la agrupación de DbContext, con AddDbContextPool. Esto siguiendo las recomendaciones de EF Core para una aplicación de ASP.NET Core.

## 5. PRUEBAS UNITARIAS
Se crea un proyecto de pruebas unitarias (API.PropertiesUS.Test) con NUnit, probando los métodos de las clases en BL mediante casos de pruebas preestablecidos.

## 6. SEGURIDAD
Se implementa una validación de Autenticación básica, esta limita el uso de los servicios sin que se registre el usuario y contraseña parametrizados. Los credenciales de acceso o autenticación se parametrizan en el archivo appsettings.json.

## 7. PRINCIPALES SERVICIOS EXPUESTOS

### 7.1.	Registrar o crear una propiedad: 
Para este caso, utilice el servicio POST /Property/Create, enviando un JSON en el body con la estructura del PropertyDTO, ej.:
```
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
En los dos últimos campos (idOwner, nameOwner) se espera un valor que indique el registro de Owner a asociar con la nueva Property.

### 7.2.	Agregar imagen de una propiedad
En este caso, se utiliza el servicio POST /PropertyImage/Add, enviando un JSON en el body con la estructura del PropertyImageDTO, ej.: 
```
	{
	  "fileImage": null,
	  "imageBase64": "/9j/4AAQSkZJRgABAQEAZABkAAD/7QNqUGhvdG9zaG9wIDMuMAA4QklNBAQAAA...",
	  "codeInternalProperty": "",
	  "nameProperty": "",
	  "idProperty": 1
	}
```
En esta estructura puede enviar una imagen de dos maneras: la primera y más recomendable es mediante el campo imageBase64, el cual espera un texto con la imagen decodificada en Base64, facilitando su registro, y la segunda manera es mediante el campo fileImage, en el cual puede enviar la imagen de tipo System.Drawing.Image.
En el campo idProperty debe enviar el identificador del registro de Property a relacionar con la imagen.

### 7.3.	Cambiar precio de una propiedad
En este caso, se utiliza el servicio POST /Property/ChangePrice, enviando un JSON en el body con la estructura del PropertyPriceDTO, ej.:
```
	{
	  "price": "550000",
	  "name": "",
	  "codeInternal": "",
	  "idProperty": 1
	}
```
En este, debe enviar el nuevo valor del precio de la propiedad en el campo Price, y para relacionar el registro de Property puede utilizar cualquiera de los últimos 3 campos (name, codeInternal, idProperty) enviando el respectivo valor.

### 7.4.	Actualizar propiedad
Para este caso, se utiliza el servicio POST /Property/Update, enviando un JSON en el body con la estructura del PropertyDTO, ej.:
```
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
Donde debe enviar, obligatoriamente, el campo idProperty con el identificador del registro de Property a actualizar. El resto de los campos se permiten actualizar.

### 7.5.	Listar propiedades con filtros de búsqueda
En este caso, se utiliza el servicio POST /Property/GetListByFilters, enviando un JSON en el body con la estructura del PropertyDTO, ej.:
```
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
En este, se podrá enviar el valor especifico que se quiere utilizar como filtro para obtener una lista de registros desde Property.
