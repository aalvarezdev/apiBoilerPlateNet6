# Flujo de promoción del laboratorio

1. Un PR hacia `Development` ejecuta `api_pr_validation`: restore, build y test sin publicar ni desplegar.
2. Un merge a `Development` ejecuta `api_ci_inicial`: build, test, publicación en ACR, bloqueo de la etiqueta del SHA y despliegue del digest a `development`.
3. La promoción a `Release` debe seleccionar el digest que ya pasó en `Development` y desplegarlo a `staging` con aprobación. No se reconstruye la imagen.
4. La promoción a `Master` debe reutilizar exactamente el mismo digest de `staging`, exigir aprobación y verificar el rollout y rollback en `production`.

`main` permanece como rama inicial y su trigger `push_main` sigue activo durante la transición. El trigger `push_development` es el camino principal nuevo. Los pasos 3 y 4 requieren la configuración de CD de los siguientes bloques; esta guía no los da por implementados.

Cada ejecución debe registrar commit, tag, digest, ambiente y resultado. La referencia de despliegue siempre usa `registro/repositorio@sha256:...`, nunca una etiqueta mutable.
