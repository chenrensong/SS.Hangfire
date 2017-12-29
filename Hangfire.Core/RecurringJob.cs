// This file is part of Hangfire.
// Copyright © 2013-2014 Sergey Odinokov.
// 
// Hangfire is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as 
// published by the Free Software Foundation, either version 3 
// of the License, or any later version.
// 
// Hangfire is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public 
// License along with Hangfire. If not, see <http://www.gnu.org/licenses/>.

using Hangfire.Common;
using Hangfire.Configuration;
using Hangfire.States;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hangfire
{
    public static class RecurringJob
    {
        private static readonly Lazy<RecurringJobManager> Instance = new Lazy<RecurringJobManager>(
            () => new RecurringJobManager());

        public static void AddOrUpdate(
            Expression<Action> methodCall,
            Func<string> cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            AddOrUpdate(methodCall, cronExpression(), timeZone, queue);
        }

        public static void AddOrUpdate<T>(
            Expression<Action<T>> methodCall,
            Func<string> cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            AddOrUpdate(methodCall, cronExpression(), timeZone, queue);
        }

        public static void AddOrUpdate(
            Expression<Action> methodCall,
            string cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            var job = Job.FromExpression(methodCall);
            var id = GetRecurringJobId(job);

            Instance.Value.AddOrUpdate(id, job, cronExpression, timeZone ?? TimeZoneInfo.Utc, queue);
        }

        public static void AddOrUpdate<T>(
            Expression<Action<T>> methodCall,
            string cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            var job = Job.FromExpression(methodCall);
            var id = GetRecurringJobId(job);

            Instance.Value.AddOrUpdate(id, job, cronExpression, timeZone ?? TimeZoneInfo.Utc, queue);
        }

        public static void AddOrUpdate(
            string recurringJobId,
            Expression<Action> methodCall,
            Func<string> cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            AddOrUpdate(recurringJobId, methodCall, cronExpression(), timeZone, queue);
        }

        public static void AddOrUpdate<T>(
            string recurringJobId,
            Expression<Action<T>> methodCall,
            Func<string> cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            AddOrUpdate(recurringJobId, methodCall, cronExpression(), timeZone, queue);
        }

        public static void AddOrUpdate(
            string recurringJobId,
            Expression<Action> methodCall,
            string cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            var job = Job.FromExpression(methodCall);
            Instance.Value.AddOrUpdate(recurringJobId, job, cronExpression, timeZone ?? TimeZoneInfo.Utc, queue);
        }

        public static void AddOrUpdate<T>(
            string recurringJobId,
            Expression<Action<T>> methodCall,
            string cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            var job = Job.FromExpression(methodCall);
            Instance.Value.AddOrUpdate(recurringJobId, job, cronExpression, timeZone ?? TimeZoneInfo.Utc, queue);
        }

        public static void AddOrUpdate(
            Expression<Func<Task>> methodCall,
            Func<string> cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            AddOrUpdate(methodCall, cronExpression(), timeZone, queue);
        }

        public static void AddOrUpdate<T>(
            Expression<Func<T, Task>> methodCall,
            Func<string> cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            AddOrUpdate(methodCall, cronExpression(), timeZone, queue);
        }

        public static void AddOrUpdate(
            Expression<Func<Task>> methodCall,
            string cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            var job = Job.FromExpression(methodCall);
            var id = GetRecurringJobId(job);

            Instance.Value.AddOrUpdate(id, job, cronExpression, timeZone ?? TimeZoneInfo.Utc, queue);
        }

        public static void AddOrUpdate<T>(
            Expression<Func<T, Task>> methodCall,
            string cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            var job = Job.FromExpression(methodCall);
            var id = GetRecurringJobId(job);

            Instance.Value.AddOrUpdate(id, job, cronExpression, timeZone ?? TimeZoneInfo.Utc, queue);
        }
        
        public static void AddOrUpdate(
            string recurringJobId,
            Expression<Func<Task>> methodCall,
            Func<string> cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            AddOrUpdate(recurringJobId, methodCall, cronExpression(), timeZone, queue);
        }

        public static void AddOrUpdate<T>(
            string recurringJobId,
            Expression<Func<T, Task>> methodCall,
            Func<string> cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            AddOrUpdate(recurringJobId, methodCall, cronExpression(), timeZone, queue);
        }

        public static void AddOrUpdate(
            string recurringJobId,
            Expression<Func<Task>> methodCall,
            string cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            var job = Job.FromExpression(methodCall);
            Instance.Value.AddOrUpdate(recurringJobId, job, cronExpression, timeZone ?? TimeZoneInfo.Utc, queue);
        }

        public static void AddOrUpdate<T>(
            string recurringJobId,
            Expression<Func<T, Task>> methodCall,
            string cronExpression,
            TimeZoneInfo timeZone = null,
            string queue = EnqueuedState.DefaultQueue)
        {
            var job = Job.FromExpression(methodCall);
            Instance.Value.AddOrUpdate(recurringJobId, job, cronExpression, timeZone ?? TimeZoneInfo.Utc, queue);
        }

        public static void RemoveIfExists(string recurringJobId)
        {
            Instance.Value.RemoveIfExists(recurringJobId);
        }

        public static void Trigger(string recurringJobId)
        {
            Instance.Value.Trigger(recurringJobId);
        }

        private static string GetRecurringJobId(Job job)
        {
            return $"{job.Type.ToGenericTypeString()}.{job.Method.Name}";
        }

        /// <summary>
        /// Builds <see cref="RecurringJob"/> automatically within specified interface or class.
        /// </summary>
        /// <param name="types">Specified interface or class</param>
        public static void AddOrUpdate(params Type[] types)
        {
            AddOrUpdate(() => types);
        }

        /// <summary>
        /// Builds <see cref="RecurringJob"/> automatically within specified interface or class.
        /// </summary>
        /// <param name="typesProvider">The provider to get specified interfaces or class.</param>
        public static void AddOrUpdate(Func<IEnumerable<Type>> typesProvider)
        {
            if (typesProvider == null) throw new ArgumentNullException(nameof(typesProvider));

            IRecurringJobBuilder builder = new RecurringJobBuilder();

            builder.Build(typesProvider);
        }

        /// <summary>
        /// Builds <see cref="RecurringJob"/> automatically by using multiple JSON configuration files.
        /// </summary>
        /// <param name="jsonFiles">The array of json files.</param>
        /// <param name="reloadOnChange">Whether the <see cref="RecurringJob"/> should be reloaded if the file changes.</param>
        public static void AddOrUpdate(string[] jsonFiles, bool reloadOnChange = true)
        {
            if (jsonFiles == null) throw new ArgumentNullException(nameof(jsonFiles));

            foreach (var jsonFile in jsonFiles)
                AddOrUpdate(jsonFile, reloadOnChange);
        }
        /// <summary>
        /// Builds <see cref="RecurringJob"/> automatically by using a JSON configuration.
        /// </summary>
        /// <param name="jsonFile">Json file for <see cref="RecurringJob"/> configuration.</param>
        /// <param name="reloadOnChange">Whether the <see cref="RecurringJob"/> should be reloaded if the file changes.</param>
        public static void AddOrUpdate(string jsonFile, bool reloadOnChange = true)
        {
            if (string.IsNullOrWhiteSpace(jsonFile)) throw new ArgumentNullException(nameof(jsonFile));

            var configFile = File.Exists(jsonFile) ? jsonFile :
                Path.Combine(
                AppContext.BaseDirectory,
                jsonFile);

            if (!File.Exists(configFile)) throw new FileNotFoundException($"The json file {configFile} does not exist.");

            IConfigurationProvider provider = new JsonConfigurationProvider(configFile, reloadOnChange);

            AddOrUpdate(provider);
        }

        /// <summary>
        /// Builds <see cref="RecurringJob"/> automatically with <seealso cref="IConfigurationProvider"/>.
        /// </summary>
        /// <param name="provider"><see cref="IConfigurationProvider"/></param>
        public static void AddOrUpdate(IConfigurationProvider provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            IRecurringJobBuilder builder = new RecurringJobBuilder();

            AddOrUpdate(provider.Load());
        }

        /// <summary>
        /// Builds <see cref="RecurringJob"/> automatically with the collection of <seealso cref="RecurringJobInfo"/>.
        /// </summary>
        /// <param name="recurringJobInfos">The collection of <see cref="RecurringJobInfo"/>.</param>
        public static void AddOrUpdate(IEnumerable<RecurringJobInfo> recurringJobInfos)
        {
            if (recurringJobInfos == null) throw new ArgumentNullException(nameof(recurringJobInfos));

            IRecurringJobBuilder builder = new RecurringJobBuilder();

            builder.Build(() => recurringJobInfos);
        }

        /// <summary>
        /// Builds <see cref="RecurringJob"/> automatically with the array of <seealso cref="RecurringJobInfo"/>.
        /// </summary>
        /// <param name="recurringJobInfos">The array of <see cref="RecurringJobInfo"/>.</param>
        public static void AddOrUpdate(params RecurringJobInfo[] recurringJobInfos)
        {
            if (recurringJobInfos == null) throw new ArgumentNullException(nameof(recurringJobInfos));

            AddOrUpdate(recurringJobInfos.AsEnumerable());
        }
    }
}
