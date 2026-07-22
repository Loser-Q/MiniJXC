<template>
  <div>
    <el-dialog v-model="state.showDialog" destroy-on-close :title="state.form.id > 0 ? '编辑商品' : '新增商品'" draggable
      :close-on-click-modal="false" :close-on-press-escape="false" width="650px">
      <el-form ref="formRef" :model="state.form" label-width="80px">
        <el-row :gutter="20">
          <el-col :xs="24" :sm="12">
            <el-form-item label="商品编号" prop="code" :rules="[{ required: true, message: '请输入编号' }]">
              <el-input v-model="state.form.code" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12">
            <el-form-item label="商品名称" prop="name" :rules="[{ required: true, message: '请输入名称' }]">
              <el-input v-model="state.form.name" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12">
            <el-form-item label="类别">
              <el-input v-model="state.form.category" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12">
            <el-form-item label="规格型号">
              <el-input v-model="state.form.specification" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="8">
            <el-form-item label="单位">
              <el-input v-model="state.form.unit" placeholder="如：个/箱/袋" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="8">
            <el-form-item label="条码">
              <el-input v-model="state.form.barcode" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="8">
            <el-form-item label="启用">
              <el-switch v-model="state.form.isEnabled" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12">
            <el-form-item label="采购价">
              <el-input-number v-model="state.form.purchasePrice" :precision="2" :min="0" style="width:100%" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12">
            <el-form-item label="销售价">
              <el-input-number v-model="state.form.salePrice" :precision="2" :min="0" style="width:100%" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12">
            <el-form-item label="初始库存">
              <el-input-number v-model="state.form.initStock" :precision="2" :min="0" style="width:100%" />
            </el-form-item>
          </el-col>
          <el-col :xs="24">
            <el-form-item label="备注">
              <el-input v-model="state.form.remark" type="textarea" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <el-button @click="state.showDialog = false">取消</el-button>
        <el-button type="primary" @click="onSure" :loading="state.sureLoading">确定</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script lang="ts" setup name="business/product/form">
import { ProductEntity } from '/@/api/admin/data-contracts'
import { ProductApi } from '/@/api/admin/Product'
import eventBus from '/@/utils/mitt'
import { FormInstance, ElMessage } from 'element-plus'

const formRef = useTemplateRef<FormInstance>('formRef')

const state = reactive({
  showDialog: false,
  sureLoading: false,
  form: {} as ProductEntity,
})

const open = async (row: any = {}) => {
  if (row.id > 0) {
    const res = await new ProductApi().get({ id: row.id })
    if (res?.data) state.form = res.data as any
  } else {
    state.form = { ...row } as any
  }
  state.showDialog = true
}

const onSure = () => {
  formRef.value!.validate(async (valid: boolean) => {
    if (!valid) return
    state.sureLoading = true
    let res: any
    if ((state.form as any).id > 0) {
      res = await new ProductApi().update(state.form as any).catch(() => { state.sureLoading = false })
    } else {
      res = await new ProductApi().add(state.form as any).catch(() => { state.sureLoading = false })
    }
    state.sureLoading = false
    if (res?.success) {
      ElMessage.success((state.form as any).id > 0 ? '修改成功' : '新增成功')
      eventBus.emit('refreshProduct')
      state.showDialog = false
    }
  })
}

defineExpose({ open })
</script>
