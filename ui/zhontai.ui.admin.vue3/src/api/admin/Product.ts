/* eslint-disable */
/* tslint:disable */
// @ts-nocheck
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

import {
  DynamicFilterInfo,
  DynamicFilterLogic,
  DynamicFilterOperator,
  ProductEntity,
  ResultOutputInt64,
  ResultOutputObject,
  ResultOutputPageOutputProductEntity,
  ResultOutputProductEntity,
  SortInput,
} from './data-contracts'
import { ContentType, HttpClient, RequestParams } from './http-client'

export class ProductApi<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags product
   * @name GetPage
   * @summary 获取商品列表
   * @request GET:/api/admin/product/get-page
   * @secure
   */
  getPage = (
    query: {
      /** 商品编号 */
      'Filter.Code': string
      /** 商品名称 */
      'Filter.Name': string
      /** 商品类别 */
      'Filter.Category'?: string
      /** 规格型号 */
      'Filter.Specification'?: string
      /** 计量单位 */
      'Filter.Unit'?: string
      /** 条码 */
      'Filter.Barcode'?: string
      /**
       * 采购价
       * @format double
       */
      'Filter.PurchasePrice'?: number
      /**
       * 销售价
       * @format double
       */
      'Filter.SalePrice'?: number
      /**
       * 初始库存数量
       * @format double
       */
      'Filter.InitStock'?: number
      /** 备注 */
      'Filter.Remark'?: string
      /** 是否启用 */
      'Filter.IsEnabled'?: boolean
      /**
       * 主键Id
       * @format int64
       */
      'Filter.Id'?: number
      /**
       * 创建时间
       * @format date-time
       */
      'Filter.CreatedTime'?: string
      /**
       * 创建者用户Id
       * @format int64
       */
      'Filter.CreatedUserId'?: number
      /** 创建者用户名 */
      'Filter.CreatedUserName'?: string
      /**
       * 修改时间
       * @format date-time
       */
      'Filter.ModifiedTime'?: string
      /**
       * 修改者用户Id
       * @format int64
       */
      'Filter.ModifiedUserId'?: number
      /** 修改者用户名 */
      'Filter.ModifiedUserName'?: string
      /**
       * 当前页标
       * @format int32
       */
      CurrentPage?: number
      /**
       * 每页大小
       * @format int32
       */
      PageSize?: number
      'DynamicFilter.Field'?: string
      /** Contains=0,StartsWith=1,EndsWith=2,NotContains=3,NotStartsWith=4,NotEndsWith=5,Equal=6,Equals=7,Eq=8,NotEqual=9,GreaterThan=10,GreaterThanOrEqual=11,LessThan=12,LessThanOrEqual=13,Range=14,DateRange=15,Any=16,NotAny=17,Custom=18 */
      'DynamicFilter.Operator'?: DynamicFilterOperator
      'DynamicFilter.Value'?: any
      /** And=0,Or=1 */
      'DynamicFilter.Logic'?: DynamicFilterLogic
      'DynamicFilter.Filters'?: DynamicFilterInfo[]
      /** 排序列表 */
      SortList?: SortInput[]
    },
    params: RequestParams = {}
  ) =>
    this.request<ResultOutputPageOutputProductEntity, any>({
      path: `/api/admin/product/get-page`,
      method: 'GET',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags product
   * @name Get
   * @summary 获取单个商品
   * @request GET:/api/admin/product/get
   * @secure
   */
  get = (
    query?: {
      /** @format int64 */
      id?: number
    },
    params: RequestParams = {}
  ) =>
    this.request<ResultOutputProductEntity, any>({
      path: `/api/admin/product/get`,
      method: 'GET',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags product
   * @name Add
   * @summary 新增商品
   * @request POST:/api/admin/product/add
   * @secure
   */
  add = (data: ProductEntity, params: RequestParams = {}) =>
    this.request<ResultOutputInt64, any>({
      path: `/api/admin/product/add`,
      method: 'POST',
      body: data,
      secure: true,
      type: ContentType.Json,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags product
   * @name Update
   * @summary 修改商品
   * @request PUT:/api/admin/product/update
   * @secure
   */
  update = (data: ProductEntity, params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/product/update`,
      method: 'PUT',
      body: data,
      secure: true,
      type: ContentType.Json,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags product
   * @name Delete
   * @summary 删除商品（软删除）
   * @request DELETE:/api/admin/product/delete
   * @secure
   */
  delete = (
    query?: {
      /** @format int64 */
      id?: number
    },
    params: RequestParams = {}
  ) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/product/delete`,
      method: 'DELETE',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags product
   * @name BatchDelete
   * @summary 批量删除商品（软删除）
   * @request PUT:/api/admin/product/batch-delete
   * @secure
   */
  batchDelete = (data: number[], params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/product/batch-delete`,
      method: 'PUT',
      body: data,
      secure: true,
      type: ContentType.Json,
      format: 'json',
      ...params,
    })
}
