using System.Text;

namespace QuickCode.Demo.Common.Workflows;

public static class WorkflowUmlGenerator
{
    public static string GenerateSequenceDiagram(this Workflow workflow)
    {
        string Sanitize(string input) => input.Replace(" ", "");

        var plantUml = new StringBuilder();
        plantUml.AppendLine("@startuml");
        plantUml.AppendLine($"title {workflow.Name} - {workflow.Version}");
        
        plantUml.AppendLine("autonumber");
        plantUml.AppendLine("actor User");

        foreach (var step in workflow.Steps)
        {
            plantUml.AppendLine($"participant \"{step.Key}\" as {Sanitize(step.Key)} <<Service>>");
        }

        var firstStep = workflow.Steps.First();
        plantUml.AppendLine($"\nUser -> {Sanitize(firstStep.Key)} ++: {workflow.Description}");

        foreach (var step in workflow.Steps)
        {
            var currentStep = step.Value;
            var stepName = Sanitize(step.Key);

            plantUml.AppendLine($"note right of {stepName}");
            plantUml.AppendLine($"API: {currentStep.Url}");
            if (currentStep.Headers.Count > 0)
            {
                plantUml.AppendLine("Headers:");
                foreach (var header in currentStep.Headers) plantUml.AppendLine($"- {header.Key}: {header.Value}");
            }

            if (currentStep.Body.Count > 0)
            {
                plantUml.AppendLine("Body:");
                foreach (var body in currentStep.Body) plantUml.AppendLine($"- {body.Key}: {body.Value}");
            }

            plantUml.AppendLine("end note");

            if (currentStep.Transitions.Any())
            {
                string transitionType = "alt";
                foreach (var transition in currentStep.Transitions)
                {
                    var actionName = Sanitize(transition.Action);
                    plantUml.AppendLine($"{transitionType} {transition.Condition}");
                    plantUml.AppendLine($"{stepName} -> {actionName} ++: Call {actionName}");


                    plantUml.AppendLine($"{actionName} --> {stepName} -- : Return {actionName}");


                    transitionType = "else";
                }

                plantUml.AppendLine("end");
            }
            
        }
        plantUml.AppendLine($"\n{Sanitize(firstStep.Key)} --> User --: {workflow.Description}");

        plantUml.AppendLine("@enduml");
        return plantUml.ToString();
    }

    public static string GenerateStateMachineDiagram(this Workflow workflow)
    {
        var plantUML = new StringBuilder();
        plantUML.AppendLine("@startuml");
        plantUML.AppendLine($"title {workflow.Name} - {workflow.Version}");
        plantUML.AppendLine();

        foreach (var stepName in workflow.Steps.Keys)
        {
            plantUML.AppendLine($"state {stepName} as \"{stepName}\"");
        }

        plantUML.AppendLine("[*] --> " + workflow.Steps.First().Value.Method);
        foreach (var stepName in workflow.Steps.Keys)
        {
            var step = workflow.Steps[stepName];
            if (step.Transitions != null)
            {
                foreach (var onSuccess in step.Transitions)
                {
                    plantUML.AppendLine($"{stepName} --> {onSuccess.Action} : {onSuccess.Condition}");
                }
            }
        }

        plantUML.AppendLine("@enduml");

        return plantUML.ToString();
    }

    public static string GenerateActivityDiagram(this Workflow workflow)
    {
        var plantUML = new StringBuilder();
        plantUML.AppendLine("@startuml");
        plantUML.AppendLine($"title {workflow.Name} - {workflow.Version}");
        plantUML.AppendLine();

        plantUML.AppendLine("start");
        foreach (var stepName in workflow.Steps.Keys)
        {
            var step = workflow.Steps[stepName];
            plantUML.AppendLine($":{stepName};");
            if (step.Transitions != null)
            {
                foreach (var onSuccess in step.Transitions)
                {
                    plantUML.AppendLine($"if ({onSuccess.Condition}) then (yes)");
                    plantUML.AppendLine($":{onSuccess.Action};");
                    plantUML.AppendLine("else (no)");
                }
            }
        }

        plantUML.AppendLine("stop");

        plantUML.AppendLine("@enduml");

        return plantUML.ToString();
    }

    public static string GetPlantUml(this Workflow workflow)
    {
        var uml = new System.Text.StringBuilder();
        uml.AppendLine("@startuml");
        uml.AppendLine($"title {workflow.Name} v{workflow.Version}");
        uml.AppendLine();
        uml.AppendLine("start");
        uml.AppendLine();

        foreach (var stepName in workflow.Steps.Keys)
        {
            var step = workflow.Steps[stepName];
            uml.AppendLine($":{stepName};");

            if (step.Body.Count > 0)
            {
                uml.AppendLine(":Body:");
                var bodyParams = new List<string>();
                foreach (var bodyParam in step.Body)
                {
                    bodyParams.Add($"  {bodyParam.Key} = {bodyParam.Value}");
                }

                uml.AppendLine($"{string.Join(Environment.NewLine, bodyParams)};");
            }

            if (step.Transitions != null)
            {
                foreach (var action in step.Transitions)
                {
                    uml.AppendLine($"if ({action.Condition}) then (yes)");
                    uml.AppendLine($"  :{action.Action};");
                    uml.AppendLine("else (no)");
                    uml.AppendLine($"  :handle{stepName}Error;");
                    uml.AppendLine("endif");
                    uml.AppendLine();
                }
            }
        }

        uml.AppendLine("stop");
        uml.AppendLine("@enduml");

        return uml.ToString();
    }

    public static string GetEncodedPlantUml(this Workflow workflow)
    {
        var umlCode = workflow.GenerateSequenceDiagram();
        byte[] data = System.Text.Encoding.UTF8.GetBytes(umlCode);
        using (var output = new MemoryStream())
        {
            using (var compressor =
                   new System.IO.Compression.DeflateStream(output, System.IO.Compression.CompressionLevel.Optimal,
                       true))
            {
                compressor.Write(data, 0, data.Length);
            }

            return Encode64(output.ToArray());
        }
    }

    private static string Encode64(byte[] data)
    {
        const string encodeTable = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-_";
        var builder = new System.Text.StringBuilder();
        uint buffer = 0;
        int bitsInBuffer = 0;

        foreach (byte b in data)
        {
            buffer = (buffer << 8) | b;
            bitsInBuffer += 8;

            while (bitsInBuffer >= 6)
            {
                bitsInBuffer -= 6;
                var index = (buffer >> bitsInBuffer) & 0x3F;

                if (index < 0 || index >= encodeTable.Length)
                    throw new InvalidOperationException("Buffer out of bounds.");

                builder.Append(encodeTable[(int)index]);
            }
        }

        if (bitsInBuffer > 0)
        {
            buffer <<= (6 - bitsInBuffer);
            var index = buffer & 0x3F;

            if (index < 0 || index >= encodeTable.Length)
                throw new InvalidOperationException("Remaining buffer out of bounds.");

            builder.Append(encodeTable[(int)index]);
        }

        return builder.ToString();
    }
}