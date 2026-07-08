CREATE DATABASE AdministracionDB;
GO

USE AdministracionDB;
GO

CREATE TABLE Departamentos (
    idDepartamento INT IDENTITY(1,1) PRIMARY KEY,
    departamento   NVARCHAR(100) NOT NULL,
    activo         BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Empleados (
    idEmpleado      INT IDENTITY(1,1) PRIMARY KEY,
    nombres         NVARCHAR(100) NOT NULL,
    apellidos       NVARCHAR(100) NOT NULL,
    sueldo          INT NOT NULL,
    cargo           NVARCHAR(100) NOT NULL,
    activo          BIT NOT NULL DEFAULT 1,
    fechaNacimiento DATE NOT NULL,
    idDepartamento  INT NOT NULL,
    CONSTRAINT FK_Empleados_Departamentos FOREIGN KEY (idDepartamento)
        REFERENCES Departamentos (idDepartamento)
);
GO

CREATE TABLE Proyectos (
    idProyecto   INT IDENTITY(1,1) PRIMARY KEY,
    proyecto     NVARCHAR(150) NOT NULL,
    fechaInicio  DATE NOT NULL,
    fechaFin     DATE NOT NULL,
    activo       BIT NOT NULL DEFAULT 1,
    presupuesto  DECIMAL(18,2) NOT NULL
);
GO

CREATE TABLE EmpleadoProyectos (
    idEmpleado      INT NOT NULL,
    idProyecto      INT NOT NULL,
    rol             NVARCHAR(100) NOT NULL,
    fechaAsignacion DATE NOT NULL,
    CONSTRAINT PK_EmpleadoProyectos PRIMARY KEY (idEmpleado, idProyecto),
    CONSTRAINT FK_EmpleadoProyectos_Empleados FOREIGN KEY (idEmpleado)
        REFERENCES Empleados (idEmpleado) ON DELETE CASCADE,
    CONSTRAINT FK_EmpleadoProyectos_Proyectos FOREIGN KEY (idProyecto)
        REFERENCES Proyectos (idProyecto) ON DELETE CASCADE
);
GO

-- Indices de apoyo para consultas frecuentes
CREATE INDEX IX_Empleados_idDepartamento ON Empleados (idDepartamento);
CREATE INDEX IX_Empleados_activo ON Empleados (activo);
CREATE INDEX IX_Proyectos_activo ON Proyectos (activo);
GO

USE AdministracionDB;
GO

INSERT INTO Departamentos (departamento, activo) VALUES
('Recursos Humanos', 1),
('Tecnología', 1),
('Finanzas', 1),
('Marketing', 1),
('Ventas', 1),
('Logística', 1),
('Compras', 1),
('Atención al Cliente', 1),
('Contabilidad', 1),
('Gerencia General', 1);
GO

INSERT INTO Empleados
(nombres, apellidos, sueldo, cargo, activo, fechaNacimiento, idDepartamento)
VALUES
('Carlos', 'Mendoza', 1200, 'Analista', 1, '1998-05-12', 2),
('María', 'González', 1500, 'Contadora', 1, '1995-09-21', 9),
('Luis', 'Ramírez', 1800, 'Desarrollador', 1, '1997-01-14', 2),
('Ana', 'Paredes', 1300, 'Asistente RRHH', 1, '1999-07-08', 1),
('José', 'Vera', 2200, 'Jefe de Ventas', 1, '1992-04-30', 5),
('Daniela', 'Morales', 1600, 'Diseñadora', 1, '1996-03-17', 4),
('Miguel', 'Cedeño', 1700, 'Supervisor Logístico', 1, '1994-08-10', 6),
('Andrea', 'López', 1400, 'Compradora', 1, '1998-11-28', 7),
('Fernando', 'Torres', 2000, 'Gerente TI', 1, '1991-06-19', 2),
('Valeria', 'Castro', 1250, 'Asesora', 1, '2000-12-03', 8);
GO

INSERT INTO Proyectos
(proyecto, fechaInicio, fechaFin, activo, presupuesto)
VALUES
('Sistema ERP', '2026-01-01', '2026-12-31', 1, 150000.00),
('Portal Web Corporativo', '2026-02-15', '2026-08-30', 1, 45000.00),
('Aplicación Móvil', '2026-03-01', '2026-10-15', 1, 60000.00),
('Migración a la Nube', '2026-01-20', '2026-09-20', 1, 90000.00),
('Automatización de Inventario', '2026-04-01', '2026-11-30', 1, 70000.00),
('CRM Empresarial', '2026-05-01', '2026-12-15', 1, 85000.00),
('Business Intelligence', '2026-02-01', '2026-09-01', 1, 50000.00),
('Sistema de Nómina', '2026-03-15', '2026-07-30', 0, 30000.00),
('E-commerce Corporativo', '2026-06-01', '2027-02-28', 1, 120000.00),
('Mesa de Ayuda', '2026-01-10', '2026-06-30', 0, 25000.00);
GO

INSERT INTO EmpleadoProyectos
(idEmpleado, idProyecto, rol, fechaAsignacion)
VALUES
(1, 1, 'Analista Funcional', '2026-01-02'),
(2, 7, 'Analista Financiero', '2026-02-05'),
(3, 3, 'Desarrollador Backend', '2026-03-03'),
(4, 8, 'Asistente RRHH', '2026-03-20'),
(5, 9, 'Líder Comercial', '2026-06-05'),
(6, 2, 'Diseñadora UI/UX', '2026-02-18'),
(7, 5, 'Supervisor Logístico', '2026-04-03'),
(8, 4, 'Compradora', '2026-01-25'),
(9, 6, 'Arquitecto de Software', '2026-05-02'),
(10, 10, 'Agente de Soporte', '2026-01-12');
GO