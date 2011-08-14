<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

        <xsl:param name="__referringPage"></xsl:param>
    
<xsl:template match="/">
    <html>
        <head>
            <title>Simple Sudoku Page</title>
        </head>

        <body>
            Input Puzzle:
            <form id="inputPuzzleForm" name="inputPuzzleForm" method="post">
                <input type="hidden" name="__referringPage" value="{$__referringPage}"/>
                <input type="hidden" name="__submittedToSelf" value="false" />

                <xsl:apply-templates select="page_xml/InputPuzzle" />

                <input type="submit" value="Find Solution" onclick="javascript:document.inputPuzzleForm.__submittedToSelf.value='true';" />
            </form>

            <br />
            <br />
            
            Solution:
            <br />
            <xsl:apply-templates select="page_xml/SolutionPuzzle" />
        </body>
    </html>
</xsl:template>

<xsl:template match="InputPuzzle">
    <table>
        <xsl:for-each select="SudokuPuzzle/Board/Row">
            <tr>
                <xsl:for-each select="Cell">
                    <td>
                        <input id="{@Row}_{@Col}" name="{@Row}_{@Col}" type="text" value="{.}" size="5" />
                    </td>
                </xsl:for-each>
            </tr>
        </xsl:for-each>
    </table>    
</xsl:template>

<xsl:template match="SolutionPuzzle">
    <xsl:choose>
        <xsl:when test="@IsSolved='True'">
            ** Solved in <xsl:value-of select="@TimeToSolve" /> milliseconds **
            <br />
            <table border="1">
                <xsl:for-each select="SudokuPuzzle/Board/Row">
                    <tr>
                        <xsl:for-each select="Cell">
                            <td style="width:53px;">
                                <xsl:value-of select="." />
                            </td>
                        </xsl:for-each>
                    </tr>
                </xsl:for-each>
            </table>
        </xsl:when>
        <xsl:when test="@IsSolved='False'">
            ** No Solution Exists **
        </xsl:when>
        <xsl:otherwise>
        </xsl:otherwise>
    </xsl:choose>
</xsl:template>

<xsl:template match="Row" mode="input">
    <tr>
        <xsl:apply-templates select="Cell" mode="input" />
    </tr>    
</xsl:template>

<xsl:template match="Row" mode="solution">
    <tr>
        <xsl:apply-templates select="Cell" mode="solution" />
    </tr>
</xsl:template>

<xsl:template match="Cell" mode="input">    
</xsl:template>

<xsl:template match="Cell" mode="solution">
</xsl:template>
    
</xsl:stylesheet>