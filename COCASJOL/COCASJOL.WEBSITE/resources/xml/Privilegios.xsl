<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="privilegios">
		<privilegios>
			<xsl:apply-templates select="privilegio" />
		</privilegios>
	</xsl:template>

	<xsl:template match="privilegio">
		<Privilegio>
			<xsl:attribute name="Key">
				<xsl:value-of select="key" />
			</xsl:attribute>
			<xsl:attribute name="Name">
				<xsl:value-of select="name" />
			</xsl:attribute>
			<xsl:attribute name="Description">
				<xsl:value-of select="description" />
			</xsl:attribute>
		</Privilegio>
	</xsl:template>
</xsl:stylesheet>