namespace QuickCode.Demo.Common.Models
{
    public class QueueSettings
    {
        /// <summary>
        /// Gets or sets the name of the host.
        /// </summary>
        /// <value>
        /// The name of the host.
        /// </value>
        public string HostName { get; set; } = default!;
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public ushort Port { get; set; }
        /// <summary>
        /// Gets or sets the virtual host.
        /// </summary>
        /// <value>
        /// The virtual host.
        /// </value>
        public string VirtualHost { get; set; } = default!;
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; } = default!;
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; } = default!;
        /// <summary>
        /// Gets or sets the name of the queue.
        /// </summary>
        /// <value>
        /// The name of the queue.
        /// </value>
        public string QueueName { get; set; } = default!;


        public string ConnectionString
        {
            get
            {
                var value = $"amqp://{UserName}:{Password}@{HostName}/{VirtualHost}";
                if (String.IsNullOrEmpty(VirtualHost))
                {
                    value = $"amqp://{UserName}:{Password}@{HostName}";
                }

                return value;
            }
        }
    }
}
