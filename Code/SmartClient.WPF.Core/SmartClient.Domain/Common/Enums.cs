using System.ComponentModel.DataAnnotations;

namespace SmartClient.Domain.Common
{
    public enum TaskUnitOfMeasure
    {
        [Display(Description = "Beats per Min (BPM)")]
        BPM,
        [Display(Description = "Duration in Minutes")]
        MIN,
        [Display(Description = "Bars or Measures")]
        BAR,
        [Display(Description = "Percentage (%)")]
        PER
    }
}
