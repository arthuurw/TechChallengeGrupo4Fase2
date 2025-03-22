namespace ContatosGrupo4.Domain.Entities;

public class Usuario : BaseEntity
{
    public required string Login { get; set; } = null!;

    public required string Senha { get; set; } = null!;

    public virtual ICollection<Contato> Contato { get; set; } = null!;
}