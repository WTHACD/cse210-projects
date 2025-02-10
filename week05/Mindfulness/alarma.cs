using cAlgo.API;

namespace cAlgo.Robots
{
    [Robot(TimeZone = TimeZones.UTC, AccessRights = AccessRights.FullAccess)]
    public class PriceAlarmBot : Robot
    {
        // Variables para controlar la alarma
        private bool alarmActive = false;
        private double alarmPrice = 0;

        // Suponiendo que ranges es un diccionario o estructura que ya manejas en tu código.
        // Aquí definimos una clase de ejemplo para ilustrar.
        public class Range
        {
            public double MinPrice { get; set; }
            public double MaxPrice { get; set; }
            public int ChangesCount { get; set; }
        }

        // Supongamos que tienes un diccionario de ranges con claves (por ejemplo, string)
        // Dictionary<string, Range> ranges = ...;

        protected override void OnStart()
        {
            // Inicializaciones necesarias
        }

        // Este método se llamaría cuando procesas cada range.
        // Puedes integrarlo donde ya estés evaluando los cambios.
        private void ProcessRange(string key, Range range)
        {
            // Si se cumplen 3 cambios, configuramos la alarma
            if (range.ChangesCount == 3)
            {
                // Por ejemplo, usamos el precio máximo del rango como referencia.
                alarmPrice = range.MaxPrice;
                alarmActive = true;
                Print("Alarma configurada para key {0}. Precio objetivo: {1}", key, alarmPrice);
            }
            else
            {
                // Puedes agregar otros casos o resetear la alarma si se requiere
            }
        }

        protected override void OnTick()
        {
            // Si la alarma está activa, verificamos el precio en cada tick.
            if (alarmActive && Symbol.Ask >= alarmPrice)
            {
                // Envia notificación push a cTrader Mobile
                Notifications.SendNotification("Alarma de Precio", $"El precio ha alcanzado {Symbol.Ask}");
                
                // También se muestra un mensaje en el terminal y se puede reproducir un sonido
                Print("¡Alarma disparada! El precio ha alcanzado {0}", Symbol.Ask);
                // PlaySound("alert.wav"); // Descomenta y asegúrate de tener el archivo si deseas reproducir un sonido.

                // Una vez disparada la alarma, la desactivamos para evitar notificaciones continuas.
                alarmActive = false;
            }
        }

        // Ejemplo de uso: Supongamos que en algún lugar de tu código iteras sobre los ranges
        private void CheckRanges()
        {
            // Ejemplo de iteración, adaptarlo a tu implementación:
            foreach (var n in /* tu colección de ranges */ new System.Collections.Generic.Dictionary<string, Range>())
            {
                // Aquí n.Key sería la clave y n.Value el objeto Range
                ProcessRange(n.Key, n.Value);
            }
        }
    }
}
