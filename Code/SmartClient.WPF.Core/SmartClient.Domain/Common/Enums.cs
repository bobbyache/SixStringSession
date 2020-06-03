using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartClient.Domain.Common
{
    public enum TaskUnitOfMeasure
    {
        [Description("Beats per Min (BPM)")]
        BPM,
        [Description("Duration in Minutes")]
        MIN,
        [Description("Bars or Measures")]
        BAR,
        [Description("Percentage (%)")]
        PER
    }
}
