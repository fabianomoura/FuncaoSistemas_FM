﻿CREATE PROC FI_SP_DelBeneficiario3
	@IDCLIENTE BIGINT	
AS
BEGIN
    DELETE BENEFICIARIOS WHERE IDCLIENTE = @IDCLIENTE;
END