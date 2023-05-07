// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using DatabaseBenchmark.Benchmarks;

var config = ManualConfig.Create(DefaultConfig.Instance)
    .WithArtifactsPath(Path.Combine("D:\\SkalowanieBenchmark_WriteSQL", "Write_2", $"{Path.GetRandomFileName()}"));

//var readCustomers = BenchmarkRunner.Run(typeof(ReadCustomersBenchmark), config);
var insertCustomers = BenchmarkRunner.Run(typeof(InsertCustomersBenchmark));
