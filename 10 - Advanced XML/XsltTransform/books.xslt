<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:b="http://library.by/catalog"
>
    <xsl:output method="html" indent="yes"/>

    <msxsl:script language="CSharp" implements-prefix="b">
       <![CDATA[
       public string DateTimeNow()
       {
          return System.DateTime.UtcNow.ToString();
       }
       ]]>
    </msxsl:script>
    
    <xsl:template match="/b:catalog">
      <html>
        <body>
          <table>
            <tr>
              <td>
                <xsl:value-of select="b:DateTimeNow()"/>
              </td>
            </tr>
            <tr>
              <td>Author</td>
              <td>Title</td>
              <td>Publish date</td>
              <td>Registration date</td>
            </tr>
          </table>
        </body>
      </html>
    </xsl:template>
  
</xsl:stylesheet>
