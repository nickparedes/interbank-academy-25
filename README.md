# Procesamiento de Transacciones Bancarias (CLI)

## Introducción
Este proyecto es una aplicación de línea de comandos (CLI) que procesa archivos CSV con transacciones bancarias y genera reportes con información relevante como balance final, transacción de mayor monto y conteo de transacciones por tipo. Fue desarrollado como parte de un reto técnico para Interbank Academy.

## Instrucciones de Ejecución

### Requisitos
- .NET 6.0 o superior
- Archivo CSV con transacciones (formato: ID,Tipo,Monto)

### Ejecución
1. Clona el repositorio o descarga el código fuente
2. Navega a la carpeta del proyecto
3. Ejecuta el programa con: dotnet run
4. Sigue las instrucciones en pantalla:
- Ingresa la ruta del archivo CSV o presiona Enter para usar la ruta por defecto (`../data.csv`)
- Selecciona las opciones del menú para generar diferentes reportes

## Enfoque y Solución

### Lógica Implementada
- **Procesamiento de CSV**: El programa lee archivos CSV con validación de formato
- **Cálculos**:
- Balance final: Suma de créditos menos débitos
- Transacción mayor monto: Búsqueda del valor máximo
- Conteo: Agrupación por tipo de transacción
- **Interfaz de usuario**: Menús interactivos con validación de entrada

### Decisiones de Diseño
- **Estructura modular**: Separación clara entre lógica de negocio, procesamiento e interfaz
- **Validaciones**: Verificación de existencia de archivos y formato correcto de datos
- **Experiencia de usuario**: Mensajes claros, manejo de errores y opción de ruta por defecto

## Estructura del Proyecto

    InterbankAcademy/
    ├── src/          # Carpeta main
    ├──────Program.cs         # Lógica principal y menús
    └── data.csv              # Archivo de ejemplo (opcional)

### Clases Principales
- `Transaccion`: Modelo de datos para las transacciones (ID, Tipo, Monto)
- `Program`: Contiene toda la lógica de procesamiento y generación de reportes

### Requisitos del formato:
- **Encabezados exactos**: `ID`, `Tipo`, `Monto` (en ese orden)
- **Tipos de transacción**: Debe ser `Crédito` o `Débito` (no sensible a mayúsculas)
- **Formato numérico**:
  - ID: Número entero
  - Monto: Decimal (punto como separador decimal)
- **Codificación**: UTF-8 (recomendado)
- **Separación**: Debe estar separado por comas

### Ejemplo mínimo válido:
```csv
ID,Tipo,Monto
1,Crédito,150.75
2,Débito,50.00
```

## Mejoras Futuras
- Utilizar el patrón de diseño strategy para tener mas ordenado el código.
- Mejorar la seguridad al procesar archivos evitando la carga de archivos maliciosos.