﻿CREATE PROC FI_SP_VerificaBeneficiarioV2
	@CPF VARCHAR(11),
	@IDCLIENTE BIGINT,
	@IDBENEFICIARIO BIGINT
AS
BEGIN
	SELECT 1 FROM BENEFICIARIOS WHERE CPF = @CPF AND IDCLIENTE = @IDCLIENTE AND ID <> @IDBENEFICIARIO
END