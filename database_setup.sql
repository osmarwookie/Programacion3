DROP TABLE IF EXISTS productos;
DROP TABLE IF EXISTS usuarios;
DROP TABLE IF EXISTS categorias;

CREATE TABLE categorias (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL UNIQUE,
    descripcion TEXT,
    activo BOOLEAN DEFAULT TRUE
);

CREATE TABLE usuarios (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(150) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    rol VARCHAR(50) DEFAULT 'Estudiante',
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE productos (
    id SERIAL PRIMARY KEY,
    categoria_id INT NOT NULL,
    nombre VARCHAR(150) NOT NULL,
    descripcion TEXT,
    precio DECIMAL(10, 2) NOT NULL CHECK (precio >= 0),
    stock INT NOT NULL DEFAULT 0 CHECK (stock >= 0),
    sku VARCHAR(50) UNIQUE,
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_categoria
        FOREIGN KEY (categoria_id)
        REFERENCES categorias(id)
        ON DELETE CASCADE
);

INSERT INTO categorias (nombre, descripcion)
VALUES
('Electrónica', 'Productos electrónicos'),
('Ropa', 'Prendas de vestir'),
('Alimentos', 'Productos alimenticios');

INSERT INTO usuarios (nombre, email, password_hash)
VALUES
('Ivan Chavez', 'ivan@example.com', '123456'),
('Admin Sistema', 'admin@example.com', 'abcdef');

INSERT INTO productos
(categoria_id, nombre, descripcion, precio, stock, sku)
VALUES
(1, 'Laptop HP', 'Laptop para oficina', 12999.99, 10, 'HP001'),
(1, 'Mouse Logitech', 'Mouse inalámbrico', 599.99, 25, 'LOG001'),
(2, 'Playera Polo', 'Playera casual', 299.99, 50, 'POL001'),
(3, 'Arroz Integral', 'Bolsa de 1kg', 39.99, 100, 'ARR001');