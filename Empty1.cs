/*
 * 由SharpDevelop创建。
 * 用户： Administrator
 * 日期: 2015/8/14
 * 时间: 0:53
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */

public class Temp { 
				static void Main() { }
				public static string Test()
				{ return String.Format(@"语音主程序 /ailn/obst/bin/call/
定报+省报 /ailn/obreport/bin/month/
kpi /ailn/kpi/obback/bin/
区域 /ailn/analysis/netmarket/bin/
　　　/ailn/obst/bin/zone/
日志
/ailn/trace/obst/{0}/{1}/
/ailn/trace/report/{0}/
报表系统程序
/ailn/obreport/bin/
报表生成文件
/ailn/report/data/out/month/{2}/

抽取文件路径	/ailn/etl/etc/obetl/

竞争对手　/ailn/obst/bin/comp
一经　　　/ailn/bass1/bin

集团报表  /data/asiainfo/chengang/zonghe/2012

hive 日志 /home/ocdc/dmpApp/proc/logs/{1}{2}/

", new object(){DateTime.Now.ToString("yyyyMM"),DateTime.Now.ToString("dd"), DateTime.Now.AddMonths(-1).ToString("yyyyMM"),,null});}}