﻿/* Copyright 2013-present MongoDB Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using FluentAssertions;
using MongoDB.Bson.TestHelpers.XunitExtensions;
using Xunit;

namespace MongoDB.Driver.Core.Configuration
{
    public class ConnectionPoolSettingsTests
    {
        private static readonly ConnectionPoolSettings __defaults = new ConnectionPoolSettings();

        [Fact]
        public void constructor_should_initialize_instance()
        {
            var subject = new ConnectionPoolSettings();

            subject.MaintenanceInterval.Should().Be(TimeSpan.FromMinutes(1));
            subject.MaxConnecting.Should().Be(2);
            subject.MaxConnections.Should().Be(100);
            subject.MinConnections.Should().Be(0);
#pragma warning disable 618
            subject.WaitQueueSize.Should().Be(500);
#pragma warning restore 618
            subject.WaitQueueTimeout.Should().Be(TimeSpan.FromMinutes(2));
        }

        [Fact]
        public void constructor_should_throw_when_maintenanceInterval_is_negative()
        {
            Action action = () => new ConnectionPoolSettings(maintenanceInterval: TimeSpan.FromSeconds(-1));

            action.ShouldThrow<ArgumentException>().And.ParamName.Should().Be("maintenanceInterval");
        }

        [Theory]
        [ParameterAttributeData]
        public void constructor_should_throw_when_maxConnecting_is_invalid([Values(0, -1)] int invalidMaxConnecting)
        {
            var exception = Record.Exception(() => new ConnectionPoolSettings(maxConnecting: invalidMaxConnecting));

            var e = exception.Should().BeOfType<ArgumentOutOfRangeException>().Subject;
            e.ParamName.Should().Be("maxConnecting");
        }

        [Theory]
        [ParameterAttributeData]
        public void constructor_should_throw_when_maxConnections_is_negative_or_zero(
            [Values(-1, 0)]
            int maxConnections)
        {
            Action action = () => new ConnectionPoolSettings(maxConnections: maxConnections);

            action.ShouldThrow<ArgumentException>().And.ParamName.Should().Be("maxConnections");
        }

        [Fact]
        public void constructor_should_throw_when_minConnections_is_negative()
        {
            Action action = () => new ConnectionPoolSettings(minConnections: -1);

            action.ShouldThrow<ArgumentException>().And.ParamName.Should().Be("minConnections");
        }

        [Fact]
        public void constructor_should_throw_when_waitQueueSize_is_negative()
        {
            Action action = () => new ConnectionPoolSettings(waitQueueSize: -1);

            action.ShouldThrow<ArgumentException>().And.ParamName.Should().Be("waitQueueSize");
        }

        [Fact]
        public void constructor_should_throw_when_waitQueueTimeout_is_negative()
        {
            Action action = () => new ConnectionPoolSettings(waitQueueTimeout: TimeSpan.FromSeconds(-1));

            action.ShouldThrow<ArgumentException>().And.ParamName.Should().Be("waitQueueTimeout");
        }

        [Fact]
        public void constructor_with_maintenanceInterval_should_initialize_instance()
        {
            var maintenanceInterval = TimeSpan.FromSeconds(123);

            var subject = new ConnectionPoolSettings(maintenanceInterval: maintenanceInterval);

            subject.MaintenanceInterval.Should().Be(maintenanceInterval);
            subject.MaxConnecting.Should().Be(__defaults.MaxConnecting);
            subject.MaxConnections.Should().Be(__defaults.MaxConnections);
            subject.MinConnections.Should().Be(__defaults.MinConnections);
#pragma warning disable 618
            subject.WaitQueueSize.Should().Be(__defaults.WaitQueueSize);
#pragma warning restore 618
            subject.WaitQueueTimeout.Should().Be(__defaults.WaitQueueTimeout);
        }

        [Fact]
        public void constructor_with_maxConnecting_should_initialize_instance()
        {
            const int maxConnecting = 1;

            var subject = new ConnectionPoolSettings(maxConnecting: maxConnecting);

            subject.MaintenanceInterval.Should().Be(__defaults.MaintenanceInterval);
            subject.MaxConnecting.Should().Be(maxConnecting);
            subject.MaxConnections.Should().Be(__defaults.MaxConnections);
            subject.MinConnections.Should().Be(__defaults.MinConnections);
#pragma warning disable 618
            subject.WaitQueueSize.Should().Be(__defaults.WaitQueueSize);
#pragma warning restore 618
            subject.WaitQueueTimeout.Should().Be(__defaults.WaitQueueTimeout);
        }

        [Fact]
        public void constructor_with_maxConnections_should_initialize_instance()
        {
            var maxConnections = 1;
            var waitQueueSize = maxConnections * 5;

            var subject = new ConnectionPoolSettings(maxConnections: maxConnections);

            subject.MaintenanceInterval.Should().Be(__defaults.MaintenanceInterval);
            subject.MaxConnecting.Should().Be(__defaults.MaxConnecting);
            subject.MaxConnections.Should().Be(maxConnections);
            subject.MinConnections.Should().Be(__defaults.MinConnections);
#pragma warning disable 618
            subject.WaitQueueSize.Should().Be(waitQueueSize);
#pragma warning restore 618
            subject.WaitQueueTimeout.Should().Be(__defaults.WaitQueueTimeout);
        }

        [Fact]
        public void constructor_with_maxConnections_and_waitQueueSize_should_initialize_instance()
        {
            var maxConnections = 1;
            var waitQueueSize = 2;

            var subject = new ConnectionPoolSettings(maxConnections: maxConnections, waitQueueSize: waitQueueSize);

            subject.MaintenanceInterval.Should().Be(__defaults.MaintenanceInterval);
            subject.MaxConnecting.Should().Be(__defaults.MaxConnecting);
            subject.MaxConnections.Should().Be(maxConnections);
            subject.MinConnections.Should().Be(__defaults.MinConnections);
#pragma warning disable 618
            subject.WaitQueueSize.Should().Be(waitQueueSize);
#pragma warning restore 618
            subject.WaitQueueTimeout.Should().Be(__defaults.WaitQueueTimeout);
        }

        [Fact]
        public void constructor_with_minConnections_should_initialize_instance()
        {
            var minConnections = 123;

            var subject = new ConnectionPoolSettings(minConnections: minConnections);

            subject.MaintenanceInterval.Should().Be(__defaults.MaintenanceInterval);
            subject.MaxConnecting.Should().Be(__defaults.MaxConnecting);
            subject.MaxConnections.Should().Be(subject.MaxConnections);
            subject.MinConnections.Should().Be(minConnections);
#pragma warning disable 618
            subject.WaitQueueSize.Should().Be(__defaults.WaitQueueSize);
#pragma warning restore 618
            subject.WaitQueueTimeout.Should().Be(__defaults.WaitQueueTimeout);
        }

        [Fact]
        public void constructor_with_waitQueueSize_should_initialize_instance()
        {
            var waitQueueSize = 123;

            var subject = new ConnectionPoolSettings(waitQueueSize: waitQueueSize);

            subject.MaintenanceInterval.Should().Be(__defaults.MaintenanceInterval);
            subject.MaxConnecting.Should().Be(__defaults.MaxConnecting);
            subject.MaxConnections.Should().Be(subject.MaxConnections);
            subject.MinConnections.Should().Be(subject.MinConnections);
#pragma warning disable 618
            subject.WaitQueueSize.Should().Be(waitQueueSize);
#pragma warning restore 618
            subject.WaitQueueTimeout.Should().Be(__defaults.WaitQueueTimeout);
        }

        [Fact]
        public void constructor_with_waitQueueTimeout_should_initialize_instance()
        {
            var waitQueueTimeout = TimeSpan.FromSeconds(123);

            var subject = new ConnectionPoolSettings(waitQueueTimeout: waitQueueTimeout);

            subject.MaintenanceInterval.Should().Be(__defaults.MaintenanceInterval);
            subject.MaxConnecting.Should().Be(__defaults.MaxConnecting);
            subject.MaxConnections.Should().Be(subject.MaxConnections);
            subject.MinConnections.Should().Be(subject.MinConnections);
#pragma warning disable 618
            subject.WaitQueueSize.Should().Be(subject.WaitQueueSize);
#pragma warning restore 618
            subject.WaitQueueTimeout.Should().Be(waitQueueTimeout);
        }

        [Fact]
        public void With_maintenanceInterval_should_return_expected_result()
        {
            var oldMaintenanceInterval = TimeSpan.FromSeconds(1);
            var newMaintenanceInterval = TimeSpan.FromSeconds(2);
            var subject = new ConnectionPoolSettings(maintenanceInterval: oldMaintenanceInterval);

            var result = subject.With(maintenanceInterval: newMaintenanceInterval);

            result.MaintenanceInterval.Should().Be(newMaintenanceInterval);
            result.MaxConnecting.Should().Be(subject.MaxConnecting);
            result.MaxConnections.Should().Be(subject.MaxConnections);
            result.MinConnections.Should().Be(subject.MinConnections);
#pragma warning disable 618
            result.WaitQueueSize.Should().Be(subject.WaitQueueSize);
#pragma warning restore 618
            result.WaitQueueTimeout.Should().Be(subject.WaitQueueTimeout);
        }

        [Fact]
        public void With_maxConnecting_should_return_expected_result()
        {
            const int oldMaxConnecting = 1;
            const int newMaxConnecting = 2;
            var subject = new ConnectionPoolSettings(maxConnecting: oldMaxConnecting);

            var result = subject.With(maxConnecting: newMaxConnecting);

            result.MaintenanceInterval.Should().Be(subject.MaintenanceInterval);
            result.MaxConnecting.Should().Be(newMaxConnecting);
            result.MaxConnections.Should().Be(subject.MaxConnections);
            result.MinConnections.Should().Be(subject.MinConnections);
#pragma warning disable 618
            result.WaitQueueSize.Should().Be(subject.WaitQueueSize);
#pragma warning restore 618
            result.WaitQueueTimeout.Should().Be(subject.WaitQueueTimeout);
        }

        [Fact]
        public void With_maxConnections_should_return_expected_result()
        {
            var oldMaxConnections = 1;
            var newMaxConnections = 2;
            var subject = new ConnectionPoolSettings(maxConnections: oldMaxConnections);

            var result = subject.With(maxConnections: newMaxConnections);

            result.MaintenanceInterval.Should().Be(subject.MaintenanceInterval);
            result.MaxConnecting.Should().Be(subject.MaxConnecting);
            result.MaxConnections.Should().Be(newMaxConnections);
            result.MinConnections.Should().Be(subject.MinConnections);
#pragma warning disable 618
            result.WaitQueueSize.Should().Be(subject.WaitQueueSize);
#pragma warning restore 618
            result.WaitQueueTimeout.Should().Be(subject.WaitQueueTimeout);
        }

        [Fact]
        public void With_minConnections_should_return_expected_result()
        {
            var oldMinConnections = 1;
            var newMinConnections = 2;
            var subject = new ConnectionPoolSettings(minConnections: oldMinConnections);

            var result = subject.With(minConnections: newMinConnections);

            result.MaintenanceInterval.Should().Be(subject.MaintenanceInterval);
            result.MaxConnecting.Should().Be(subject.MaxConnecting);
            result.MaxConnections.Should().Be(subject.MaxConnections);
            result.MinConnections.Should().Be(newMinConnections);
#pragma warning disable 618
            result.WaitQueueSize.Should().Be(subject.WaitQueueSize);
#pragma warning restore 618
            result.WaitQueueTimeout.Should().Be(subject.WaitQueueTimeout);
        }

        [Fact]
        public void With_waitQueueSizes_should_return_expected_result()
        {
            var oldWaitQueueSize = 1;
            var newWaitQueueSize = 2;
            var subject = new ConnectionPoolSettings(waitQueueSize: oldWaitQueueSize);

            var result = subject.With(waitQueueSize: newWaitQueueSize);

            result.MaintenanceInterval.Should().Be(subject.MaintenanceInterval);
            result.MaxConnecting.Should().Be(subject.MaxConnecting);
            result.MaxConnections.Should().Be(subject.MaxConnections);
            result.MinConnections.Should().Be(subject.MinConnections);
#pragma warning disable 618
            result.WaitQueueSize.Should().Be(newWaitQueueSize);
#pragma warning restore 618
            result.WaitQueueTimeout.Should().Be(subject.WaitQueueTimeout);
        }

        [Fact]
        public void With_waitQueueTimeoutl_should_return_expected_result()
        {
            var oldWaitQueueTimeout = TimeSpan.FromSeconds(1);
            var newWaitQueueTimeout = TimeSpan.FromSeconds(2);
            var subject = new ConnectionPoolSettings(waitQueueTimeout: oldWaitQueueTimeout);

            var result = subject.With(waitQueueTimeout: newWaitQueueTimeout);

            result.MaintenanceInterval.Should().Be(subject.MaintenanceInterval);
            result.MaxConnections.Should().Be(subject.MaxConnections);
            result.MinConnections.Should().Be(subject.MinConnections);
#pragma warning disable 618
            result.WaitQueueSize.Should().Be(subject.WaitQueueSize);
#pragma warning restore 618
            result.WaitQueueTimeout.Should().Be(newWaitQueueTimeout);
        }

        [Fact]
        public void WithInternal_should_return_expected_result()
        {
            var subject = new ConnectionPoolSettings();

            var result = subject.WithInternal(isPausable: false);
            result.IsPausable.Should().Be(false);
            result = subject.WithInternal(isPausable: true);
            result.IsPausable.Should().Be(true);
        }
    }
}
