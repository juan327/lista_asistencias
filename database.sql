CREATE DATABASE lista_asistencias;
GO

USE lista_asistencias;
GO

CREATE TABLE usuarios
(
  id_usuario INT IDENTITY(1,1) PRIMARY KEY,
  nombres VARCHAR(50) NOT NULL,
  apellidos VARCHAR(50) NOT NULL,
  correo VARCHAR(50)
);

GO

CREATE TABLE reuniones
(
  id_reunion INT IDENTITY(1,1) PRIMARY KEY,
  nombre VARCHAR(200) NOT NULL,
  descripcion TEXT,
  fecha VARCHAR(20)
);

GO

CREATE TABLE asistencias
(
  id_asistencia INT IDENTITY(1,1) PRIMARY KEY,
  reunion INT NOT NULL,
  usuario INT NOT NULL,
  CONSTRAINT fk_usuario_asistencias FOREIGN KEY (usuario)
  REFERENCES usuarios(id_usuario),
  CONSTRAINT fk_reuniones_asistencias FOREIGN KEY (reunion)
  REFERENCES reuniones(id_reunion)
)