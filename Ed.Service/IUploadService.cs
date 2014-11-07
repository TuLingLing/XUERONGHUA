using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Ed.Service
{
    public interface IUploadService
    {
        /// <summary>
        /// 裁剪图片并保存
        /// </summary>
        bool cropSaveAs(string fileName, string newFileName, int maxWidth, int maxHeight, int cropWidth,
            int cropHeight, int X, int Y);



        /// <summary>
        /// 文件上传方法A
        /// </summary>
        /// <param name="postedFile">文件流</param>
        /// <param name="isThumbnail">是否生成缩略图</param>
        /// <param name="isWater">是否打水印</param>
        /// <returns>服务器文件路径</returns>
        string fileSaveAs(HttpPostedFileBase postedFile, bool isThumbnail, bool isWater);

        /// <summary>
        /// 文件上传方法B
        /// </summary>
        /// <param name="postedFile">文件流</param>
        /// <param name="isThumbnail">是否生成缩略图</param>
        /// <param name="isWater">是否打水印</param>
        /// <param name="isImage">是否必须上传图片</param>
        /// <returns>服务器文件路径</returns>
        string fileSaveAs(HttpPostedFileBase postedFile, bool isThumbnail, bool isWater, bool _isImage);
         /// <summary>
        /// 文件上传方法C
        /// </summary>
        /// <param name="postedFile">文件流</param>
        /// <param name="isThumbnail">是否生成缩略图</param>
        /// <param name="isWater">是否打水印</param>
        /// <param name="isReOriginal">是否返回文件原名称</param>
        /// <returns>服务器文件路径</returns>
         string fileSaveAs(HttpPostedFileBase postedFile, bool isThumbnail, bool isWater, bool _isImage, bool _isReOriginal);


    }
}