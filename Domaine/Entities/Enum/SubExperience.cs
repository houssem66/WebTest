using System.ComponentModel.DataAnnotations;

namespace TourMe.Data.Entities.Enum
{
    public enum SubExperience
    {
        //culture
        [SubcategoryOf(TypeExperience.Culture)]
        [Display(Name = "Cours sur l'entrepreneuriat")] Coursentrepreneuriat,
        [SubcategoryOf(TypeExperience.Culture)]
        [Display(Name = "Conférence cultulrelle")] Conférencecultulrelle,
        [SubcategoryOf(TypeExperience.Culture)]
        [Display(Name = "cours de longue")]
        CoursDelangue,
        [SubcategoryOf(TypeExperience.Culture)]
        [Display(Name = "cours de longue")]
        CoursdeScience,
        [SubcategoryOf(TypeExperience.Culture)]
        [Display(Name = "Conférence sur des enjeux")]
        ConférenceEnjeu,
        [SubcategoryOf(TypeExperience.Culture)]
        [Display(Name = "Danse culturelle")]
        DanseCulturelle,
        [SubcategoryOf(TypeExperience.Culture)]
        [Display(Name = "Visite de bureau")]
        VisiteBureau,
        [SubcategoryOf(TypeExperience.Culture)]
        [Display(Name = "Festivale culturellle")]
        FestivaleCulturelle,
        [SubcategoryOf(TypeExperience.Culture)]
        [Display(Name = "Visite d'usine")]
        VisiteUsine,
        //Food
        [SubcategoryOf(TypeExperience.Food)]
        [Display(Name = "Cuisine et alimentation")]
        CuisineAlimentation,
        [SubcategoryOf(TypeExperience.Food)]
        [Display(Name = "Dégustation gastronomique")]
        DegustationGastro,
        [SubcategoryOf(TypeExperience.Food)]
        [Display(Name = "Autre")]
        autre,
        //[SubcategoryOf(TypeExperience.Food)]
        //[Display(Name = "")],


    }
}
