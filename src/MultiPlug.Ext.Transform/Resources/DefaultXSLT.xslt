<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">
    <!-- 3 Ways to Select a Subject Value
      1. By it's Name
      Subject Value: <xsl:value-of select="root/subjects/NameOfSubject"/>
      
      2. By it's Index Value
      Subject Value: <xsl:value-of select="root/index/item0"/>
    
      3. Using the Array
      <xsl:for-each select="root/array">
        Subject Value:  <xsl:value-of select="." />
      </xsl:for-each> -->
  </xsl:template>
</xsl:stylesheet>