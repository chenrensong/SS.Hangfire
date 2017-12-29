using System;
using System.Collections.Generic;
using Hangfire.Configuration;

namespace Hangfire
{
	/// <summary>
	/// Hangfire <see cref="RecurringJob"/> extensions.
	/// </summary>
	public static class HangfireExtensions
	{
		/// <summary>
		/// Builds <see cref="RecurringJob"/> automatically within specified interface or class.
		/// To the Hangfire client, alternatively way is to use the class <seealso cref="CronJob"/> to add or update <see cref="RecurringJob"/>.
		/// </summary>
		/// <param name="configuration"><see cref="IGlobalConfiguration"/></param>
		/// <param name="types">Specified interface or class</param>
		/// <returns><see cref="IGlobalConfiguration"/></returns>
		public static IGlobalConfiguration UseRecurringJob(this IGlobalConfiguration configuration, params Type[] types)
		{
			return UseRecurringJob(configuration, () => types);
		}

		/// <summary>
		/// Builds <see cref="RecurringJob"/> automatically within specified interface or class.
		/// To the Hangfire client, alternatively way is to use the class <seealso cref="CronJob"/> to add or update <see cref="RecurringJob"/>.
		/// </summary>
		/// <param name="configuration"><see cref="IGlobalConfiguration"/></param>
		/// <param name="typesProvider">The provider to get specified interfaces or class.</param>
		/// <returns><see cref="IGlobalConfiguration"/></returns>
		public static IGlobalConfiguration UseRecurringJob(this IGlobalConfiguration configuration, Func<IEnumerable<Type>> typesProvider)
		{
			if (typesProvider == null) throw new ArgumentNullException(nameof(typesProvider));

            RecurringJob.AddOrUpdate(typesProvider);

			return configuration;
		}
		/// <summary>
		/// Builds <see cref="RecurringJob"/> automatically by using a JSON configuration.
		/// To the Hangfire client, alternatively way is to use the class <seealso cref="CronJob"/> to add or update <see cref="RecurringJob"/>.
		/// </summary>
		/// <param name="configuration"><see cref="IGlobalConfiguration"/>.</param>
		/// <param name="jsonFile">Json file for <see cref="RecurringJob"/> configuration.</param>
		/// <param name="reloadOnChange">Whether the <see cref="RecurringJob"/> should be reloaded if the file changes.</param>
		/// <returns><see cref="IGlobalConfiguration"/></returns>
		public static IGlobalConfiguration UseRecurringJob(this IGlobalConfiguration configuration, string jsonFile, bool reloadOnChange = true)
		{
			if (string.IsNullOrWhiteSpace(jsonFile)) throw new ArgumentNullException(nameof(jsonFile));

            RecurringJob.AddOrUpdate(jsonFile, reloadOnChange);

			return configuration;
		}

		/// <summary>
		/// Builds <see cref="RecurringJob"/> automatically by using multiple JSON configuration files.
		/// To the Hangfire client, alternatively way is to use the class <seealso cref="CronJob"/> to add or update <see cref="RecurringJob"/>.
		/// </summary>
		/// <param name="configuration"><see cref="IGlobalConfiguration"/>.</param>
		/// <param name="jsonFiles">The array of json files.</param>
		/// <param name="reloadOnChange">Whether the <see cref="RecurringJob"/> should be reloaded if the file changes.</param>
		/// <returns></returns>
		public static IGlobalConfiguration UseRecurringJob(this IGlobalConfiguration configuration, string[] jsonFiles, bool reloadOnChange = true)
		{
			if (jsonFiles == null) throw new ArgumentNullException(nameof(jsonFiles));

            RecurringJob.AddOrUpdate(jsonFiles, reloadOnChange);

			return configuration;
		}

		/// <summary>
		/// Builds <see cref="RecurringJob"/> automatically with <seealso cref="IConfigurationProvider"/>.
		/// To the Hangfire client, alternatively way is to use the class <seealso cref="CronJob"/> to add or update <see cref="RecurringJob"/>.
		/// </summary>
		/// <param name="configuration"><see cref="IGlobalConfiguration"/>.</param>
		/// <param name="provider"><see cref="IConfigurationProvider"/></param>
		/// <returns><see cref="IGlobalConfiguration"/>.</returns>
		public static IGlobalConfiguration UseRecurringJob(this IGlobalConfiguration configuration, IConfigurationProvider provider)
		{
			if (provider == null) throw new ArgumentNullException(nameof(provider));

            RecurringJob.AddOrUpdate(provider);

			return configuration;
		}
	}
}
