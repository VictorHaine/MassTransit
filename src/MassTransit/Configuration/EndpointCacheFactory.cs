﻿// Copyright 2007-2011 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit
{
	using System;
	using EndpointConfigurators;
	using Magnum;
	using Transports;
	using Util;

	public static class EndpointCacheFactory
	{
		static readonly EndpointFactoryDefaultSettings _defaultSettings = new EndpointFactoryDefaultSettings();

		[NotNull]
		public static IEndpointCache New([NotNull] Action<EndpointFactoryConfigurator> configure)
		{
			Guard.AgainstNull(configure, "configure");

			var configurator = new EndpointFactoryConfiguratorImpl(_defaultSettings);

			configure(configurator);

			configurator.Validate();

			IEndpointFactory endpointFactory = configurator.CreateEndpointFactory();

			IEndpointCache endpointCache = new EndpointCache(endpointFactory);

			return endpointCache;
		}

		public static void ConfigureDefaultSettings([NotNull] Action<EndpointFactoryDefaultSettingsConfigurator> configure)
		{
			Guard.AgainstNull(configure);

			var configurator = new EndpointFactoryDefaultSettingsConfiguratorImpl(_defaultSettings);

			configure(configurator);
		}
	}
}