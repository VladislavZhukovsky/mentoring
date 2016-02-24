<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:b="http://library.by/catalog"
>
    <xsl:output method="html" indent="yes"/>

    <xsl:template match="/b:catalog">
      <html>
        <body>
          <table>
            <xsl:apply-templates/>
          </table>
        </body>
      </html>
    </xsl:template>

  <xsl:template match="b:book/b:author">
    
  </xsl:template>
  
</xsl:stylesheet>
