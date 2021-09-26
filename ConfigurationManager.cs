namespace System.Configuration
{
    //
    // Resumen:
    //     Proporciona acceso a los archivos de configuración para las aplicaciones cliente.
    //     Esta clase no puede heredarse.
    public static class ConfigurationManager
    {
        //
        // Resumen:
        //     Obtiene los datos System.Configuration.AppSettingsSection para la configuración
        //     predeterminada de la aplicación actual.
        //
        // Devuelve:
        //     Devuelve un objeto System.Collections.Specialized.NameValueCollection que incluye
        //     el contenido del objeto System.Configuration.AppSettingsSection para la configuración
        //     predeterminada de la aplicación actual.
        //
        // Excepciones:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     No se pudo recuperar un objeto System.Collections.Specialized.NameValueCollection
        //     con los datos de configuración de la aplicación.
        public static NameValueCollection AppSettings { get; }
        //
        // Resumen:
        //     Obtiene los datos System.Configuration.ConnectionStringsSection para la configuración
        //     predeterminada de la aplicación actual.
        //
        // Devuelve:
        //     Devuelve un objeto System.Configuration.ConnectionStringSettingsCollection que
        //     incluye el contenido del objeto System.Configuration.ConnectionStringsSection
        //     para la configuración predeterminada de la aplicación actual.
        //
        // Excepciones:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     No se pudo recuperar un System.Configuration.ConnectionStringSettingsCollection
        //     objeto.
        public static ConnectionStringSettingsCollection ConnectionStrings { get; }

        //
        // Resumen:
        //     Recupera una sección de configuración especificada para la configuración predeterminada
        //     de la aplicación actual.
        //
        // Parámetros:
        //   sectionName:
        //     Ruta de acceso y nombre de la sección de configuración.
        //
        // Devuelve:
        //     Objeto System.Configuration.ConfigurationSection especificado o null si la sección
        //     no existe.
        //
        // Excepciones:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     No se pudo cargar un archivo de configuración.
        public static object GetSection(string sectionName);
        //
        // Resumen:
        //     Abre el archivo de configuración para la aplicación actual como un objeto System.Configuration.Configuration.
        //
        // Parámetros:
        //   userLevel:
        //     Objeto System.Configuration.ConfigurationUserLevel para el que se está abriendo
        //     la configuración.
        //
        // Devuelve:
        //     Objeto System.Configuration.Configuration.
        //
        // Excepciones:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     No se pudo cargar un archivo de configuración.
        public static Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel);
        //
        // Resumen:
        //     Abre el archivo de configuración de cliente especificado como un objeto System.Configuration.Configuration.
        //
        // Parámetros:
        //   exePath:
        //     Ruta de acceso del archivo ejecutable (exe).
        //
        // Devuelve:
        //     Objeto System.Configuration.Configuration.
        //
        // Excepciones:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     No se pudo cargar un archivo de configuración.
        public static Configuration OpenExeConfiguration(string exePath);
        //
        // Resumen:
        //     Abre el archivo de configuración del equipo como un objeto System.Configuration.Configuration
        //     en el equipo actual.
        //
        // Devuelve:
        //     Objeto System.Configuration.Configuration.
        //
        // Excepciones:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     No se pudo cargar un archivo de configuración.
        public static Configuration OpenMachineConfiguration();
        //
        // Resumen:
        //     Abre el archivo de configuración de cliente especificado como un objeto System.Configuration.Configuration
        //     que utiliza la asignación de archivos y el nivel de usuario indicados.
        //
        // Parámetros:
        //   fileMap:
        //     Objeto System.Configuration.ExeConfigurationFileMap que hace referencia al archivo
        //     de configuración que se va a usar en lugar del archivo de configuración predeterminada
        //     de la aplicación.
        //
        //   userLevel:
        //     Objeto System.Configuration.ConfigurationUserLevel para el que se abre la configuración.
        //
        // Devuelve:
        //     Objeto de configuración.
        //
        // Excepciones:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     No se pudo cargar un archivo de configuración.
        public static Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel);
        //
        // Resumen:
        //     Abre el archivo de configuración de cliente especificado como un objeto System.Configuration.Configuration
        //     que usa la asignación de archivos, nivel de usuario y opción de carga previa
        //     especificados.
        //
        // Parámetros:
        //   fileMap:
        //     Objeto System.Configuration.ExeConfigurationFileMap que hace referencia al archivo
        //     de configuración que se va a utilizar en lugar del archivo de configuración de
        //     la aplicación predeterminada.
        //
        //   userLevel:
        //     Objeto System.Configuration.ConfigurationUserLevel para el que se abre la configuración.
        //
        //   preLoad:
        //     true para cargar previamente todos los grupos de secciones y secciones; de lo
        //     contrario, false.
        //
        // Devuelve:
        //     Objeto de configuración.
        //
        // Excepciones:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     No se pudo cargar un archivo de configuración.
        public static Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel, bool preLoad);
        //
        // Resumen:
        //     Abre el archivo de configuración del equipo como un objeto System.Configuration.Configuration
        //     que utiliza la asignación de archivos especificada.
        //
        // Parámetros:
        //   fileMap:
        //     Objeto System.Configuration.ExeConfigurationFileMap que hace referencia al archivo
        //     de configuración que se va a usar en lugar del archivo de configuración predeterminada
        //     de la aplicación.
        //
        // Devuelve:
        //     Objeto System.Configuration.Configuration.
        //
        // Excepciones:
        //   T:System.Configuration.ConfigurationErrorsException:
        //     No se pudo cargar un archivo de configuración.
        public static Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap);
        //
        // Resumen:
        //     Actualiza la sección con nombre para que se vuelva a leer desde el disco la próxima
        //     vez que se recupere.
        //
        // Parámetros:
        //   sectionName:
        //     Nombre de la sección de configuración o ruta de acceso y nombre de sección de
        //     configuración de la sección que se va a actualizar.
        public static void RefreshSection(string sectionName);
    }
}