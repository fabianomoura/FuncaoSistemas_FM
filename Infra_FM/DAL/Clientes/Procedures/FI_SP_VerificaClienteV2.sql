﻿CREATE PROC FI_SP_VerificaClienteV2
	@CPF VARCHAR(11),
	@ID BIGINT
AS
BEGIN
	SELECT 1 FROM CLIENTES WHERE CPF = @CPF and ID <> @ID
END