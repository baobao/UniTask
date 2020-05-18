﻿using Cysharp.Threading.Tasks;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using Cysharp.Threading.Tasks.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreTests
{
    public class AsyncReactivePropertyTest
    {
        [Fact]
        public async Task Iteration()
        {
            var rp = new AsyncReactiveProperty<int>(99);

            var f = await rp.FirstAsync();
            f.Should().Be(99);

            var array = rp.Take(5).ToArrayAsync();

            rp.Value = 100;
            rp.Value = 100;
            rp.Value = 100;
            rp.Value = 131;

            var ar = await array;

            ar.Should().BeEquivalentTo(new[] { 99, 100, 100, 100, 131 });
        }

        [Fact]
        public async Task WithoutCurrent()
        {
            var rp = new AsyncReactiveProperty<int>(99);

            var array = rp.WithoutCurrent().Take(5).ToArrayAsync();

            rp.Value = 100;
            rp.Value = 100;
            rp.Value = 100;
            rp.Value = 131;
            rp.Value = 191;

            var ar = await array;

            ar.Should().BeEquivalentTo(new[] { 100, 100, 100, 131, 191 });
        }
    }


}
