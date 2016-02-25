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

            <xsl:call-template name="tableHead1">
              <xsl:with-param name="genreName">Computer</xsl:with-param>
            </xsl:call-template>

            <xsl:call-template name="tableHead2"/>

            <xsl:call-template name="tableBody">
              <xsl:with-param name="genreName">Computer</xsl:with-param>
            </xsl:call-template>

            <!--<xsl:call-template name="table">
              <xsl:with-param name="genreName">Computer</xsl:with-param>
            </xsl:call-template>-->
            
          </table>
        </body>
      </html>
    </xsl:template>


  <xsl:template name="table">
    <xsl:param name="genreName"></xsl:param>
    
    <xsl:call-template name="tableHead1">
      <xsl:with-param name="genreName">
        <xsl:value-of select="$genreName"/>
      </xsl:with-param>
    </xsl:call-template>
    
    <xsl:call-template name="tableHead2"/>
    
    <xsl:call-template name="tableBody">
      <xsl:with-param name="genreName">$genreName</xsl:with-param>
    </xsl:call-template>
    
  </xsl:template>

  <xsl:template name="tableHead1">
    <xsl:param name="genreName"/>
    <tr>
      <td align="right">
        <xsl:text>Genre: </xsl:text>
      </td>
      <td>
        <xsl:value-of select="$genreName"></xsl:value-of>
      </td>
      <td align="right">
        <xsl:text>Date: </xsl:text>
      </td>
      <td>
        <xsl:value-of select="b:DateTimeNow()"/>
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="tableHead2">
    <tr>
      <td align="center">Author</td>
      <td align="center">Title</td>
      <td align="center">Publish date</td>
      <td align="center">Registration date</td>
    </tr>
  </xsl:template>

  <xsl:template name="tableBody">
    <xsl:param name="genreName"></xsl:param>
    <xsl:for-each select="b:book[b:genre=$genreName]">
      <tr>
        <td>
          <xsl:value-of select="b:author"/>
        </td>
        <td>
          <xsl:value-of select="b:title"/>
        </td>
        <td>
          <xsl:value-of select="b:publish_date"/>
        </td>
        <td>
          <xsl:value-of select="b:registration_date"/>
        </td>
      </tr>
    </xsl:for-each>
  </xsl:template>

</xsl:stylesheet>
