CREATE PROC FI_SP_PesqBeneficiario
    @idCliente bigint,
	@iniciarEm int,
	@quantidade int,
	@campoOrdenacao varchar(200),
	@crescente bit	
AS
BEGIN
	DECLARE @SCRIPT NVARCHAR(MAX)
	DECLARE @CAMPOS NVARCHAR(MAX)
	DECLARE @ORDER VARCHAR(50)
	
	IF(@campoOrdenacao = 'NOME')
		SET @ORDER =  ' NOME '
	ELSE
		SET @ORDER = ' CPF '

	IF(@crescente = 0)
		SET @ORDER = @ORDER + ' DESC'
	ELSE
		SET @ORDER = @ORDER + ' ASC'

	SET @CAMPOS = '@iniciarEm int,@quantidade int'
	SET @SCRIPT = 
	'SELECT ID, NOME, CPF FROM
		(SELECT ROW_NUMBER() OVER (ORDER BY ' + @ORDER + ') AS Row, ID, NOME, CPF FROM BENEFICIARIOS WITH(NOLOCK))
		AS BeneficiariosWithRowNumbers
	WHERE Row > @iniciarEm AND Row <= (@iniciarEm+@quantidade) and IDCLIENTE = '+ @idCliente +' ORDER BY'
	
	SET @SCRIPT = @SCRIPT + @ORDER
			
	EXECUTE SP_EXECUTESQL @SCRIPT, @CAMPOS, @iniciarEm, @quantidade

	SELECT COUNT(1) FROM BENEFICIARIOS WITH(NOLOCK)
END