﻿using StepByStep.Core.ExpressionEvaluators;

namespace StepByStep.Core.Steps.Variables.InitializeVariable
{
    public sealed class InitializeVariableStepHandler : IStepHandler
    {
        public Task Handle(IStep step, Automat automat, IExpressionEvaluator expressionEvaluator)
        {
            var initializeVariableStep = (InitializeVariableStep)step;
            if (initializeVariableStep.Variable.Value is null)
            {
                automat.Variables.Add(initializeVariableStep.Variable);
                return Task.CompletedTask;
            }

            var result = expressionEvaluator.Evaluate(initializeVariableStep.Variable.Value, VariableType.String, automat.Variables);
            initializeVariableStep.Variable.SetValue(result.Value);

            automat.Variables.Add(initializeVariableStep.Variable);

            return Task.CompletedTask;
        }
    }
}
