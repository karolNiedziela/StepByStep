namespace StepByStep.Api
{
    internal static class InputTest
    {
        public static string Input => @"
{
    ""Name"": ""First automat"",
  ""Steps"": [
    {
      ""DisplayName"": ""Initialize firstname"",
      ""TypeName"": ""InitializeVariableStep"",
      ""AssemblyQualifiedName"": ""StepByStep.Core.Steps.Variables.InitializeVariable.InitializeVariableStep, StepByStep.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
      ""Variable"": {
        ""Name"": ""First Name"",
        ""VariableType"": 2,
        ""Value"": ""Karol""
      }
    },
    {
      ""DisplayName"": ""Initialize lastname"",
      ""TypeName"": ""InitializeVariableStep"",
      ""AssemblyQualifiedName"": ""StepByStep.Core.Steps.Variables.InitializeVariable.InitializeVariableStep, StepByStep.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
      ""Variable"": {
        ""Name"": ""Last Name"",
        ""VariableType"": 2,
        ""Value"": ""Niedziela""
      }
    },
    {
      ""DisplayName"": ""Initialize empty"",
      ""TypeName"": ""InitializeVariableStep"",
      ""AssemblyQualifiedName"": ""StepByStep.Core.Steps.Variables.InitializeVariable.InitializeVariableStep, StepByStep.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
      ""Variable"": {
        ""Name"": ""Empty"",
        ""VariableType"": 2,
        ""Value"": null
      }
    },
    {
      ""DisplayName"": ""Set value"",
      ""TypeName"": ""SetVariableValueStep"",
      ""AssemblyQualifiedName"": ""StepByStep.Core.Steps.Variables.SetVariable.SetVariableValueStep, StepByStep.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
      ""For"": {
        ""Name"": ""Empty"",
        ""VariableType"": 2,
        ""Value"": null
      },
      ""Value"": {
        ""Name"": ""Last Name"",
        ""VariableType"": 2,
        ""Value"": ""Niedziela""
      }
    },
    {
      ""DisplayName"": ""Initialize fullname"",
      ""TypeName"": ""InitializeVariableStep"",
      ""AssemblyQualifiedName"": ""StepByStep.Core.Steps.Variables.InitializeVariable.InitializeVariableStep, StepByStep.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
      ""Variable"": {
        ""Name"": ""Full name"",
        ""VariableType"": 2,
        ""Value"": ""@concat(First Name, \u0027 \u0027, Last Name)""
      }
    }
  ]
}";
    }
}
