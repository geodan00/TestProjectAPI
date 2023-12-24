namespace TestGeodanApi.DTO
{
    public record struct PersonCreateDto(string Name, string CreateBy, List<PersonCreateSectorDto> Sectors);
}
