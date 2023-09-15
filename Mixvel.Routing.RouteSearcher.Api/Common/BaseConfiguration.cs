using System.Collections.Generic;

namespace Mixvel.Routing.RouteSearcher.Api.Common;

    /// <summary>
    /// Описание базовой конфигурации сервиса
    /// </summary>
    public class BaseConfiguration
    {
        /// <summary>
        /// Признак режима разработки — включен swagger
        /// </summary>
        public bool UseDevelopmentPages { get; set; } = false;

        /// <summary>
        /// Признак наличия авторизации. Позволяет отправлять токены доступа через swagger
        /// </summary>
        public bool UseSwaggerAuthorization { get; set; } = false;

        /// <summary>
        /// Список разрешенных источников для CORS
        /// </summary>
        public List<string> AllowedOrigins { get; set; }

        /// <summary>
        /// Список разрешенных заголовков для CORS
        /// </summary>
        public List<string> AllowedHeaders { get; set; }

        /// <summary>
        /// Список разрешенных методов для CORS
        /// </summary>
        public List<string> AllowedMethods { get; set; }

        /// <summary>
        /// Разрешается принимать идентификационные данные от клиента (например, куки)
        /// </summary>
        public bool UseAllowCredentials { get; set; } = true;

        /// <summary>
        /// Использовать ли сжатие ответа
        /// </summary>
        public bool UseResponseCompression { get; set; } = false;
        
        /// <summary>
        /// Версия сервиса
        /// </summary>
        public string Version { get; set; }
    }
