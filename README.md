# Guía de Trabajo con Ramas en el Repositorio
Estructura de Ramas
Main Branch (main):

Esta es la rama principal. Contiene la versión estable del código.
No se debe trabajar directamente en esta rama.
Solo el administrador tiene permiso para hacer merge en esta rama.
Development Branch (development):

Esta es la rama de desarrollo principal. Todas las nuevas funcionalidades y correcciones deben integrarse aquí primero.
Usamos esta rama para probar y asegurar la calidad antes de fusionar con main.
Feature Branches (feature/*):

Para el desarrollo de nuevas funcionalidades.
Cada funcionalidad o tarea debe desarrollarse en su propia rama feature/*.
Ejemplo: feature/login-system, feature/appointment-booking.
Flujo de Trabajo
1. Crear una Nueva Rama de Funcionalidad
Antes de comenzar a trabajar en una nueva tarea o funcionalidad, crea una nueva rama a partir de development:

bash
Copiar código
# Cambia a la rama development
git checkout development

# Asegúrate de tener los últimos cambios
git pull origin development

# Crea una nueva rama de funcionalidad
git checkout -b feature/nombre-de-la-funcionalidad
2. Trabajar en la Nueva Funcionalidad
Realiza tus cambios y realiza commits regularmente:

bash
Copiar código
# Agregar los cambios
git add .

# Realizar un commit
git commit -m "Descripción de los cambios"
3. Subir la Rama al Repositorio Remoto
Sube tu rama de funcionalidad al repositorio remoto para que otros puedan verla:

bash
Copiar código
git push origin feature/nombre-de-la-funcionalidad
4. Crear un Pull Request
Cuando hayas terminado tu trabajo en la rama de funcionalidad, crea un Pull Request (PR) para fusionar tus cambios en development:

Ve al repositorio en GitHub.
Navega a la pestaña "Pull requests".
Haz clic en "New pull request".
Selecciona development como la rama base y tu rama de funcionalidad como la rama de comparación.
Revisa los cambios y crea el PR.
5. Revisión de Código
Al crear el PR, otros miembros del equipo deben revisar tu código:

Los revisores pueden dejar comentarios, solicitar cambios o aprobar el PR.
Realiza los cambios solicitados y actualiza el PR si es necesario.
6. Fusionar el Pull Request
Una vez que tu PR ha sido aprobado:

El administrador fusionará el PR en la rama development.
No se debe hacer merge directo a main.
7. Mantener la Rama development Actualizada
Es importante mantener tu rama development actualizada con los últimos cambios de main:

bash
Copiar código
# Cambia a la rama development
git checkout development

# Asegúrate de tener los últimos cambios de main
git pull origin main

# Actualiza la rama development
git merge main
8. Eliminar la Rama de Funcionalidad
Después de fusionar tu PR, elimina la rama de funcionalidad localmente y en el remoto:

bash
Copiar código
# Eliminar la rama localmente
git branch -d feature/nombre-de-la-funcionalidad

# Eliminar la rama en el remoto
git push origin --delete feature/nombre-de-la-funcionalidad
Buenas Prácticas
Realiza commits pequeños y frecuentes.
Escribe mensajes de commit claros y descriptivos.
Mantén tu rama de funcionalidad enfocada en una tarea específica.
Sincroniza frecuentemente con la rama development para evitar conflictos grandes.
