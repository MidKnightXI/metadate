﻿using System.CommandLine;

namespace WhichCam;

internal static class Program
{
    private static int Main(string[] args)
    {
        var rootCommand = new RootCommand("MetaDate - Date detector");
        var target = new Option<DirectoryInfo>(
            name: "--target",
            description: "The directory containing the images to analyze."){
            IsRequired = true };
        var output = new Option<FileInfo>(
            name: "--output",
            description: "The output file path."){
            IsRequired = true };

        rootCommand.AddOption(target);
        rootCommand.AddOption(output);

        rootCommand.SetHandler((targ, outp) =>
        {
            if (InfosExtractor.Check(targ) is false)
                return;

            var infos = InfosExtractor.RetrieveInformation(targ);
            InfosExtractor.SaveOutputInformation(infos, outp);
        }, target, output);

        return rootCommand.Invoke(args);
    }
}